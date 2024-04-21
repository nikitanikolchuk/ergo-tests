using System.Windows.Input;
using TestAdministration.Models.Data;
using TestAdministration.Models.Storages;
using Wpf.Ui.Controls;
using Wpf.Ui.Input;

namespace TestAdministration.ViewModels;

/// <summary>
/// A view model for choosing patients and conducting tests.
/// </summary>
public class TestingViewModel(
    ITestStorage testStorage,
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
                _currentPatient = null;
                return;
            }

            var patient = testStorage.GetPatientById(value.Id);
            if (patient is null)
            {
                _alertInvalidPatientFile();
                return;
            }

            CurrentViewModel = new TestConductionViewModel();
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
}