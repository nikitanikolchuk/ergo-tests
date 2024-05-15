using System.Windows.Input;
using TestAdministration.Models.Data;
using TestAdministration.Models.Storages;
using Wpf.Ui.Controls;
using Wpf.Ui.Input;

namespace TestAdministration.ViewModels.Testing;

/// <summary>
/// A view model for choosing patient from a dynamic list.
/// </summary>
public class PatientChoiceViewModel : ViewModelBase
{
    private readonly ITestStorage _testStorage;
    private readonly List<PatientDirectoryInfo> _patients;
    private readonly Action<Patient> _onChoosePatient;
    private readonly Action _onOpenAddPatient;

    private string _searchText = string.Empty;

    /// <summary>
    /// Sets dependencies and adds an anonymous patient if
    /// patient list is empty.
    /// </summary>
    public PatientChoiceViewModel(
        ITestStorage testStorage,
        Action<Patient> onChoosePatient,
        Action onOpenAddPatient
    )
    {
        _testStorage = testStorage;
        _onChoosePatient = onChoosePatient;
        _onOpenAddPatient = onOpenAddPatient;
        _patients = _testStorage.GetAllPatientDirectoryInfos();
        if (_patients.Count != 0)
        {
            return;
        }

        var anonymousPatient = _createAnonymousPatient();
        _testStorage.AddPatient(anonymousPatient);
        _patients = _testStorage.GetAllPatientDirectoryInfos();
    }

    public List<PatientDirectoryInfo> Patients => _filteredPatients();

    public string SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(Patients));
        }
    }

    public PatientDirectoryInfo? SelectedPatient
    {
        set
        {
            if (value is null)
            {
                return;
            }

            var patient = _testStorage.GetPatient(value);
            if (patient is null)
            {
                _alertInvalidPatientFile();
                return;
            }

            _onChoosePatient(patient);
        }
    }

    public ICommand OnOpenAddPatient => new RelayCommand<object?>(_ => _onOpenAddPatient());

    private List<PatientDirectoryInfo> _filteredPatients() => _patients.Where(
        patient => patient.Surname.StartsWith(SearchText, StringComparison.CurrentCultureIgnoreCase)
    ).ToList();

    private static Patient _createAnonymousPatient() => new(
        "0",
        "Anonym",
        "Anonym",
        true,
        new DateOnly(1970, 1, 1),
        Hand.Right,
        Hand.Right
    );

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