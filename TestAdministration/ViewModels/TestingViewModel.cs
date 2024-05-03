using TestAdministration.Models.Data;
using TestAdministration.Models.Storages;
using TestAdministration.ViewModels.Results;

namespace TestAdministration.ViewModels;

/// <summary>
/// A view model for choosing patients and conducting tests.
/// </summary>
public class TestingViewModel : ViewModelBase
{
    private readonly ITestStorage _testStorage;
    private readonly TestConductionViewModelFactory _testConductionViewModelFactory;
    private readonly ResultsViewModelFactory _resultsViewModelFactory;
    private readonly TestType _testType;

    private ViewModelBase _currentViewModel;

    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        private set
        {
            _currentViewModel = value;
            OnPropertyChanged();
        }
    }

    public TestingViewModel(
        ITestStorage testStorage,
        TestConductionViewModelFactory testConductionViewModelFactory,
        ResultsViewModelFactory resultsViewModelFactory,
        TestType testType
    )
    {
        _testConductionViewModelFactory = testConductionViewModelFactory;
        _resultsViewModelFactory = resultsViewModelFactory;
        _testStorage = testStorage;
        _testType = testType;
        _currentViewModel = new PatientChoiceViewModel(_testStorage, _onStartTesting, _onOpenAddPatient);
    }

    private void _onOpenPatientChoice() =>
        CurrentViewModel = new PatientChoiceViewModel(_testStorage, _onStartTesting, _onOpenAddPatient);

    private void _onOpenAddPatient() =>
        CurrentViewModel = new NewPatientViewModel(_testStorage, _onOpenPatientChoice);

    private void _onStartTesting(Patient patient) =>
        CurrentViewModel = _testConductionViewModelFactory.Create(patient, _testType, _onShowResults);

    private void _onShowResults(Patient patient, Test test) =>
        CurrentViewModel = _resultsViewModelFactory.Create(patient, test, _onSaveTest);

    private void _onSaveTest(Patient patient, Test test)
    {
        _testStorage.AddTest(patient, test);
        _onOpenPatientChoice();
    }
}