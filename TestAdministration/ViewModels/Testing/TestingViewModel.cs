using TestAdministration.Models.Data;
using TestAdministration.Models.Storages;
using TestAdministration.ViewModels.Testing.Results;
using Wpf.Ui;

namespace TestAdministration.ViewModels.Testing;

/// <summary>
/// A view model for choosing patients and conducting tests.
/// </summary>
public class TestingViewModel : ViewModelBase
{
    private readonly IContentDialogService _contentDialogService;
    private readonly ITestStorage _testStorage;
    private readonly TestConductionViewModelFactory _testConductionViewModelFactory;
    private readonly ResultsViewModelFactory _resultsViewModelFactory;
    private readonly TestType _testType;

    private ViewModelBase _currentViewModel;

    public TestingViewModel(
        IContentDialogService contentDialogService,
        ITestStorage testStorage,
        TestConductionViewModelFactory testConductionViewModelFactory,
        ResultsViewModelFactory resultsViewModelFactory,
        TestType testType
    )
    {
        _contentDialogService = contentDialogService;
        _testConductionViewModelFactory = testConductionViewModelFactory;
        _resultsViewModelFactory = resultsViewModelFactory;
        _testStorage = testStorage;
        _testType = testType;
        _currentViewModel = new PatientChoiceViewModel(_contentDialogService, _testStorage, _onStartTesting, _onOpenAddPatient);
    }

    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        private set
        {
            _currentViewModel = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Is true if there may be unsaved data.
    /// </summary>
    public bool IsBlockingNavigation => CurrentViewModel is not PatientChoiceViewModel;

    private void _onOpenPatientChoice() =>
        CurrentViewModel = new PatientChoiceViewModel(_contentDialogService, _testStorage, _onStartTesting, _onOpenAddPatient);

    private void _onOpenAddPatient() =>
        CurrentViewModel = new NewPatientViewModel(_testStorage, _onOpenPatientChoice);

    private void _onStartTesting(Patient patient, int trialCount) =>
        CurrentViewModel = _testConductionViewModelFactory.Create(patient, trialCount, _testType, _onShowResults);

    private void _onShowResults(Patient patient, Test test) =>
        CurrentViewModel = _resultsViewModelFactory.Create(patient, test, _onSaveTest);

    private void _onSaveTest(Patient patient, Test test, List<string> videoFilePaths)
    {
        _testStorage.AddTest(patient, test, videoFilePaths);
        _onOpenPatientChoice();
    }
}