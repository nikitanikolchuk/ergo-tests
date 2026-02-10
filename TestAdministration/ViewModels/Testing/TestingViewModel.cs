using System.Windows;
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
    private readonly LayoutStateViewModel _layoutState;
    private readonly TestConductionViewModelFactory _testConductionViewModelFactory;
    private readonly ResultsViewModelFactory _resultsViewModelFactory;
    private readonly TestType _testType;
    private readonly Action<bool> _setIsNavPaneOpen;
    private readonly Action<Visibility> _setHeaderVisibility;

    public TestingViewModel(
        IContentDialogService contentDialogService,
        ITestStorage testStorage,
        LayoutStateViewModel layoutStateViewModel,
        TestConductionViewModelFactory testConductionViewModelFactory,
        ResultsViewModelFactory resultsViewModelFactory,
        TestType testType,
        Action<bool> setIsNavPaneOpen,
        Action<Visibility> setHeaderVisibility
    )
    {
        _contentDialogService = contentDialogService;
        _layoutState = layoutStateViewModel;
        _testConductionViewModelFactory = testConductionViewModelFactory;
        _resultsViewModelFactory = resultsViewModelFactory;
        _testStorage = testStorage;
        _testType = testType;
        _setIsNavPaneOpen = setIsNavPaneOpen;
        _setHeaderVisibility = setHeaderVisibility;
        
        _layoutState.OnLayoutUpdated += _updateHeaderVisibility;

        CurrentViewModel = new PatientChoiceViewModel(_contentDialogService, _testStorage, _onStartTesting, _onOpenAddPatient);
    }

    public ViewModelBase CurrentViewModel
    {
        get;
        private set
        {
            field = value;
            _updateHeaderVisibility(_layoutState.IsCompactLayout);
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

    private void _onStartTesting(Patient patient, int trialCount)
    {
        _setIsNavPaneOpen(false);
        CurrentViewModel = _testConductionViewModelFactory.Create(patient, trialCount, _testType, _onShowResults);
    }

    private void _onShowResults(Patient patient, Test test) =>
        CurrentViewModel = _resultsViewModelFactory.Create(patient, test, _onSaveTest);

    private void _onSaveTest(Patient patient, Test test, List<string> videoFilePaths)
    {
        _setIsNavPaneOpen(true);
        _testStorage.AddTest(patient, test, videoFilePaths);
        _onOpenPatientChoice();
    }

    private void _updateHeaderVisibility(bool isCompactLayout)
    {
        var headerVisibility = isCompactLayout && CurrentViewModel is TestConductionViewModel
            ? Visibility.Collapsed
            : Visibility.Visible;
        _setHeaderVisibility(headerVisibility);
    }
}