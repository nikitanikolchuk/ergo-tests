using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Input;
using TestAdministration.Models.Data;
using TestAdministration.Models.TestBuilders;
using TestAdministration.Models.Utils;
using TestAdministration.ViewModels.Instructions;
using TestAdministration.ViewModels.Instructions.Bbt;
using TestAdministration.ViewModels.Instructions.Nhpt;
using TestAdministration.ViewModels.Instructions.Ppt;
using TestAdministration.ViewModels.Rules;
using Wpf.Ui.Input;

namespace TestAdministration.ViewModels;

/// <summary>
/// A view model for conducting tests that includes getting
/// instructions and inputting values.
/// </summary>
public partial class TestConductionViewModel : ViewModelBase
{
    private const string Culture = "cs";
    private const int MaxAnnulations = 3;

    [GeneratedRegex(@"^(?!0\d)\d+,?\d*$")]
    private static partial Regex FloatRegex();

    private readonly ITestBuilder _testBuilder;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly Action<Test> _saveTest;
    private float? _currentValue;
    private string _currentValueString = string.Empty;
    private int _annulationCount;
    private string _currentNote = string.Empty;

    public TestConductionViewModel(
        ITestBuilderFactory testBuilderFactory,
        IDateTimeProvider dateTimeProvider,
        string tester,
        Patient patient,
        TestType testType,
        Action<Test> saveTest)
    {
        _dateTimeProvider = dateTimeProvider;
        _testBuilder = testBuilderFactory.Create(testType)
            .SetTester(tester)
            .SetPatient(patient)
            .SetDate(_dateTimeProvider.Today)
            .SetStartTime(_dateTimeProvider.Now);
        _saveTest = saveTest;
        TitleViewModel = new TestConductionTitleViewModel(_testBuilder, patient.DominantHand);
        InstructionsViewModel = _getInstructionsViewModel(_testBuilder, patient.DominantHand);
        RulesViewModel = _getRulesViewModel(testType);
    }

    public TestConductionTitleViewModel TitleViewModel { get; }
    public IInstructionsViewModel InstructionsViewModel { get; }
    public string ValuePlaceholderText => _getValuePlaceholderText(_testBuilder.Type, _testBuilder.CurrentSection);

    public string CurrentValue
    {
        get => _currentValueString;
        set
        {
            if (value == string.Empty)
            {
                _currentValue = null;
                _currentValueString = value;
                OnPropertyChanged();
                return;
            }

            if (!FloatRegex().Match(value).Success)
            {
                return;
            }

            try
            {
                _currentValue = float.Parse(value, CultureInfo.GetCultureInfo(Culture));
                _currentValueString = value;
            }
            catch (FormatException)
            {
                return;
            }

            OnPropertyChanged();
        }
    }

    public bool IsValueReadOnly => AnnulationCount >= MaxAnnulations;

    public int AnnulationCount
    {
        get => _annulationCount;
        set
        {
            _annulationCount = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsValueReadOnly));
        }
    }

    public string CurrentNote
    {
        get => _currentNote;
        set
        {
            _currentNote = value;
            OnPropertyChanged();
        }
    }

    public ViewModelBase RulesViewModel { get; }

    // ReSharper disable once UnusedMember.Global
    public string SelectedRule
    {
        set => CurrentNote += value;
    }

    public ICommand OnIncrementAnnulations => new RelayCommand<object?>(_ => _onIncrementAnnulations());
    public ICommand OnAddValue => new RelayCommand<object?>(_ => _onAddValue());
    public ICommand OnFinishTesting => new RelayCommand<object?>(_ => _onFinishTesting());

    private static IInstructionsViewModel _getInstructionsViewModel(ITestBuilder testBuilder, Hand dominantHand) =>
        testBuilder.Type switch
        {
            TestType.Nhpt => new NhptInstructionsViewModel(testBuilder, dominantHand),
            TestType.Ppt => new PptInstructionsViewModel(testBuilder, dominantHand),
            TestType.Bbt => new BbtInstructionsViewModel(testBuilder, dominantHand),
            _ => throw new InvalidEnumArgumentException(
                nameof(testBuilder.Type),
                Convert.ToInt32(testBuilder.Type),
                typeof(TestType)
            )
        };

    private static ViewModelBase _getRulesViewModel(TestType testType) => testType switch
    {
        TestType.Nhpt => new NhptRulesViewModel(),
        TestType.Ppt => new PptRulesViewModel(),
        TestType.Bbt => new BbtRulesViewModel(),
        _ => throw new InvalidEnumArgumentException(
            nameof(testType),
            Convert.ToInt32(testType),
            typeof(TestType)
        )
    };

    private static string _getValuePlaceholderText(TestType testType, int section) => testType switch
    {
        TestType.Nhpt => "Čas v sekundách",
        TestType.Ppt => _getPptValuePlaceholderText(section),
        TestType.Bbt => "Počet kostek",
        _ => throw new InvalidEnumArgumentException(
            nameof(testType),
            Convert.ToInt32(testType),
            typeof(TestType)
        )
    };

    private static string _getPptValuePlaceholderText(int section) => section switch
    {
        0 => "Počet kolíků",
        1 => "Počet kolíků",
        2 => "Počet párů kolíků",
        3 => "Počet součástek",
        _ => throw new ArgumentOutOfRangeException(
            nameof(section),
            section,
            "PPT input section number not in range 0..3"
        )
    };

    private void _onIncrementAnnulations()
    {
        if (AnnulationCount >= MaxAnnulations)
        {
            return;
        }

        AnnulationCount++;
        CurrentValue = string.Empty;
    }

    private void _onAddValue()
    {
        _testBuilder.AddValue(_currentValue, CurrentNote);

        TitleViewModel.OnPropertyChanged(nameof(TitleViewModel.CurrentSection));
        TitleViewModel.OnPropertyChanged(nameof(TitleViewModel.CurrentTrial));
        InstructionsViewModel.OnPropertyChanged(nameof(InstructionsViewModel.CurrentViewModel));
        OnPropertyChanged(nameof(ValuePlaceholderText));
        CurrentValue = string.Empty;
        CurrentNote = string.Empty;
        AnnulationCount = 0;

        if (_testBuilder.IsFinished)
        {
            _onFinishTesting();
        }
    }

    private void _onFinishTesting()
    {
        _testBuilder.SetEndTime(_dateTimeProvider.Now);
        var test = _testBuilder.Build();
        _saveTest(test);
    }
}