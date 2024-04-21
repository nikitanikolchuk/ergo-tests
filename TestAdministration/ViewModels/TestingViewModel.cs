using System.Windows.Input;
using TestAdministration.Models.Data;
using TestAdministration.Models.Storages;
using Wpf.Ui.Input;

namespace TestAdministration.ViewModels;

public class TestingViewModel(
    ITestStorage testStorage,
    TestType testType
) : ViewModelBase
{
    private ViewModelBase _currentViewModel = new PatientChoiceViewModel(testStorage);

    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        private set
        {
            _currentViewModel = value;
            OnPropertyChanged();
        }
    }

    public ICommand OnOpenPatientChoice => new RelayCommand<object?>(_ =>
        CurrentViewModel = new PatientChoiceViewModel(testStorage)
    );

    public ICommand OnOpenAddPatient => new RelayCommand<object?>(_ =>
        CurrentViewModel = new NewPatientViewModel(testStorage)
    );

    public ICommand OnAddPatient => new RelayCommand<Func<bool>>(_onAddPatient);

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
}