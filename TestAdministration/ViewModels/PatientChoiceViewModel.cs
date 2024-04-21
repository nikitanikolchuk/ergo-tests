using TestAdministration.Models.Data;
using TestAdministration.Models.Storages;

namespace TestAdministration.ViewModels;

/// <summary>
/// A view model for choosing patient from a dynamic list.
/// </summary>
public class PatientChoiceViewModel(
    ITestStorage testStorage
) : ViewModelBase
{
    private readonly List<PatientDirectoryInfo> _patients = [..testStorage.GetAllPatientDirectoryInfos()];
    private string _searchText = string.Empty;

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
}