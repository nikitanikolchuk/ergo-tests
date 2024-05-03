using TestAdministration.Models.Data;
using TestAdministration.Models.Storages;

namespace TestAdministration.ViewModels;

/// <summary>
/// A view model for choosing patient from a dynamic list.
/// </summary>
public class PatientChoiceViewModel : ViewModelBase
{
    private readonly List<PatientDirectoryInfo> _patients;
    private string _searchText = string.Empty;

    public PatientChoiceViewModel(ITestStorage testStorage)
    {
        _patients = testStorage.GetAllPatientDirectoryInfos();
        if (_patients.Count != 0)
        {
            return;
        }

        var anonymousPatient = _createAnonymousPatient();
        testStorage.AddPatient(anonymousPatient);
        _patients = testStorage.GetAllPatientDirectoryInfos();
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
}