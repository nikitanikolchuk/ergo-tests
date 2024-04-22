using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Input;
using TestAdministration.Models.Data;
using TestAdministration.Models.TestBuilders;
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

    private readonly TestBuilder _testBuilder;
    private float? _currentValue;
    private string _currentValueString = string.Empty;
    private int _annulationCount;
    private string _currentNote = string.Empty;

    public TestConductionViewModel(TestBuilder testBuilder, string tester, Patient patient)
    {
        _testBuilder = testBuilder;
        _testBuilder.SetTester(tester);
        _testBuilder.SetPatient(patient);
    }

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

    public ICommand OnIncrementAnnulations => new RelayCommand<object?>(_ => _onIncrementAnnulations());
    public ICommand OnAddValue => new RelayCommand<object?>(_ => _onAddValue());

    private void _onIncrementAnnulations()
    {
        if (AnnulationCount >= MaxAnnulations)
        {
            return;
        }

        AnnulationCount++;
        CurrentValue = string.Empty;
        OnPropertyChanged(nameof(IsValueReadOnly));
    }

    private void _onAddValue()
    {
        _testBuilder.AddValue(_currentValue, CurrentNote);
        CurrentValue = string.Empty;
        CurrentNote = string.Empty;
        AnnulationCount = 0;

        // TODO: add test saving
    }
}