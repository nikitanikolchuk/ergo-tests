using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Input;
using TestAdministration.Models.Data;
using TestAdministration.Models.Services;
using TestAdministration.Models.TestBuilders;
using TestAdministration.Models.Utils;
using TestAdministration.ViewModels.Instructions;
using TestAdministration.ViewModels.Instructions.Bbt;
using TestAdministration.ViewModels.Instructions.Nhpt;
using TestAdministration.ViewModels.Instructions.Ppt;
using TestAdministration.ViewModels.Rules;
using Wpf.Ui;
using Wpf.Ui.Controls;
using Wpf.Ui.Input;
using static TestAdministration.Models.Storages.Records.CsvRecordConfiguration;

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

    private readonly IContentDialogService _contentDialogService;
    private readonly AudioInstructionService _audioInstructionService;
    private readonly VideoRecorderService _videoRecorderService;
    private readonly ITestBuilder _testBuilder;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly Patient _patient;
    private readonly Action<Patient, Test> _onShowResults;

    private float? _currentValue;
    private string _currentValueString = string.Empty;
    private int _annulationCount;
    private string _currentNote = string.Empty;

    public TestConductionViewModel(
        IContentDialogService contentDialogService,
        AudioInstructionService audioInstructionService,
        VideoRecorderService videoRecorderService,
        ITestBuilderFactory testBuilderFactory,
        IDateTimeProvider dateTimeProvider,
        string tester,
        Patient patient,
        TestType testType,
        Action<Patient, Test> onShowResults
    )
    {
        _contentDialogService = contentDialogService;
        _audioInstructionService = audioInstructionService;
        _videoRecorderService = videoRecorderService;
        _dateTimeProvider = dateTimeProvider;
        _patient = patient;
        _testBuilder = testBuilderFactory.Create(testType)
            .SetTester(tester)
            .SetPatient(_patient)
            .SetDate(_dateTimeProvider.Today)
            .SetStartTime(_dateTimeProvider.Now);
        _onShowResults = onShowResults;

        TitleViewModel = new TestConductionTitleViewModel(_testBuilder, _patient.DominantHand);
        InstructionsViewModel = _getInstructionsViewModel(_audioInstructionService, _testBuilder, _patient);
        CameraFeedViewModel = new CameraFeedViewModel(_videoRecorderService);
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
    public string AnnulationProgress => $"{AnnulationCount}/{MaxAnnulations}";

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
    public CameraFeedViewModel CameraFeedViewModel { get; }

    // ReSharper disable once UnusedMember.Global
    public string SelectedRule
    {
        set => CurrentNote += value;
    }

    public ICommand OnIncrementAnnulations => new RelayCommand<ContentDialog>(_onIncrementAnnulations);
    public ICommand OnAddValue => new RelayCommand<object?>(_ => _onAddValue());
    public ICommand OnRemoveValue => new RelayCommand<object?>(_ => _onRemoveValue());
    public ICommand OnPauseRecording => new RelayCommand<object?>(_ => CameraFeedViewModel.OnPauseRecording());
    public ICommand OnFinishTestingEarly => new RelayCommand<object?>(_ => _onFinishTestingEarly());

    private int AnnulationCount
    {
        get => _annulationCount;
        set
        {
            _annulationCount = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsValueReadOnly));
            OnPropertyChanged(nameof(AnnulationProgress));
        }
    }

    private static IInstructionsViewModel _getInstructionsViewModel(
        AudioInstructionService audioService,
        ITestBuilder testBuilder,
        Patient patient
    ) => testBuilder.Type switch
    {
        TestType.Nhpt => new NhptInstructionsViewModel(audioService, testBuilder, patient),
        TestType.Ppt => new PptInstructionsViewModel(audioService, testBuilder, patient),
        TestType.Bbt => new BbtInstructionsViewModel(audioService, testBuilder, patient),
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

    private async void _onIncrementAnnulations(ContentDialog? nhptAnnulationDialog)
    {
        if (nhptAnnulationDialog is null)
        {
            throw new ArgumentException("NHPT annulation dialog is null");
        }

        if (AnnulationCount >= MaxAnnulations)
        {
            return;
        }

        if (_testBuilder.Type == TestType.Nhpt)
        {
            _ = await _contentDialogService.ShowAsync(nhptAnnulationDialog, CancellationToken.None);
        }

        AnnulationCount++;
        CurrentValue = string.Empty;
    }

    private void _onAddValue()
    {
        _testBuilder.AddValue(_currentValue, CurrentNote);
        _resetContent();

        if (_testBuilder.IsFinished)
        {
            _onFinishTesting();
        }
    }

    private void _onRemoveValue()
    {
        _testBuilder.RemoveValue(out var value, out var note);
        _resetContent();

        CurrentValue = value is not null
            ? value.Value.ToString(FloatFormat, new CultureInfo(Culture))
            : string.Empty;
        CurrentNote = note;
    }

    private void _resetContent()
    {
        TitleViewModel.OnPropertyChanged(nameof(TitleViewModel.CurrentSection));
        TitleViewModel.OnPropertyChanged(nameof(TitleViewModel.CurrentTrial));
        _audioInstructionService.Stop();
        InstructionsViewModel.OnPropertyChanged(nameof(InstructionsViewModel.CurrentViewModel));
        OnPropertyChanged(nameof(ValuePlaceholderText));
        CurrentValue = string.Empty;
        CurrentNote = string.Empty;
        AnnulationCount = 0;
    }

    private async void _onFinishTestingEarly()
    {
        var messageBox = new MessageBox
        {
            Title = "Upozornění",
            Content = "Opravdu chcete předčasně ukončit testování?" +
                      " Tímto uložíte poslední hodnotu a budete přesměrován(a) na stránku s výsledky",
            PrimaryButtonText = "Potvrdit",
            CloseButtonText = "Zrušit"
        };

        var messageBoxResult = await messageBox.ShowDialogAsync();
        if (messageBoxResult != MessageBoxResult.Primary)
        {
            return;
        }

        _testBuilder.AddValue(_currentValue, CurrentNote);
        _onFinishTesting();
    }

    private void _onFinishTesting()
    {
        _testBuilder.SetEndTime(_dateTimeProvider.Now);
        var test = _testBuilder.Build();
        CameraFeedViewModel.OnStopRecording();
        _onShowResults(_patient, test);
    }
}