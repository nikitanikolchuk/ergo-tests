using System.Windows.Input;
using TestAdministration.Models.Data;
using TestAdministration.Models.Services;
using TestAdministration.Models.Storages;
using TestAdministration.Models.Storages.Converters;
using TestAdministration.Models.TestBuilders;
using TestAdministration.Models.Utils;
using TestAdministration.ViewModels.Results;
using Wpf.Ui.Controls;
using Wpf.Ui.Input;

namespace TestAdministration.ViewModels;

/// <summary>
/// A view model for choosing patients and conducting tests.
/// </summary>
public class TestingViewModel(
    ConfigurationService configurationService,
    AudioInstructionService audioInstructionService,
    ITestBuilderFactory testBuilderFactory,
    ITestStorage testStorage,
    DocumentationConverter documentationConverter,
    IDateTimeProvider dateTimeProvider,
    AgeCalculatorService ageCalculatorService,
    TestType testType
) : ViewModelBase
{
    private ViewModelBase _currentViewModel = new PatientChoiceViewModel(testStorage);
    private PatientDirectoryInfo? _currentPatientDirectoryInfo;
    private Patient? _currentPatient;

    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        private set
        {
            _currentViewModel = value;
            OnPropertyChanged();
        }
    }

    // ReSharper disable once UnusedMember.Global
    public PatientDirectoryInfo? SelectedPatient
    {
        get => _currentPatientDirectoryInfo;
        set
        {
            _currentPatientDirectoryInfo = value;
            OnPropertyChanged();

            if (value is null)
            {
                return;
            }

            _onStartTesting(value.Id);
        }
    }

    public ICommand OnOpenPatientChoice => new RelayCommand<object?>(_ =>
        CurrentViewModel = new PatientChoiceViewModel(testStorage)
    );

    public ICommand OnOpenAddPatient => new RelayCommand<object?>(_ =>
        CurrentViewModel = new NewPatientViewModel(testStorage)
    );

    // ReSharper disable once UnusedMember.Global
    public ICommand OnAddPatient => new RelayCommand<Func<bool>>(_onAddPatient);

    private static async void _alertInvalidPatientFile()
    {
        var messageBox = new MessageBox
        {
            Title = "Chyba",
            Content = "Nesprávný formát souboru s osobními údaji pacienta nebo chybějící soubor. " +
                      "Otevřete návod k použití pro více informace",
            CloseButtonText = "Zavřít"
        };

        await messageBox.ShowDialogAsync();
    }

    private void _onAddPatient(Func<bool>? addPatient)
    {
        if (addPatient == null)
        {
            throw new ArgumentException("Function for building patient is null");
        }

        var isSuccessful = addPatient();
        if (isSuccessful)
        {
            CurrentViewModel = new PatientChoiceViewModel(testStorage);
        }
    }

    private void _onStartTesting(string patientId)
    {
        var patient = testStorage.GetPatientById(patientId);
        if (patient is null)
        {
            _alertInvalidPatientFile();
            return;
        }

        _currentPatient = patient;
        var tester = configurationService.CurrentUser;
        CurrentViewModel = new TestConductionViewModel(
            audioInstructionService,
            testBuilderFactory,
            dateTimeProvider,
            tester,
            patient,
            testType,
            _onShowResults
        );
    }

    private void _onShowResults(Test test)
    {
        if (_currentPatient is null)
        {
            throw new InvalidOperationException("Go to results used without a chosen patient");
        }

        var previousTest = testStorage.GetLastTestByPatientId(test.Type, _currentPatient.Id);
        var patientAge = ageCalculatorService.Calculate(_currentPatient);
        CurrentViewModel = new ResultsViewModel(
            documentationConverter,
            _currentPatient,
            patientAge,
            test,
            previousTest,
            () => _onSaveTest(test)
        );
    }

    private void _onSaveTest(Test test)
    {
        if (_currentPatient is null)
        {
            throw new InvalidOperationException("Save test used without a chosen patient");
        }

        testStorage.AddTest(_currentPatient, test);
        SelectedPatient = null;
        _currentPatient = null;
        CurrentViewModel = new PatientChoiceViewModel(testStorage);
    }
}