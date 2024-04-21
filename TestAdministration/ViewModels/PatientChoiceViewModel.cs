using System.Collections.ObjectModel;
using TestAdministration.Models.Data;
using TestAdministration.Models.Storages;

namespace TestAdministration.ViewModels;

public class PatientChoiceViewModel(
    ITestStorage testStorage
) : ViewModelBase
{
    private readonly List<PatientDirectoryInfo> _patients = [..testStorage.GetAllPatientsShort()];
    private string _searchText = string.Empty;
    private PatientDirectoryInfo? _selectedPatient;

    public ObservableCollection<PatientDirectoryInfo> Patients => _filteredPatients();

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
        get => _selectedPatient;
        set
        {
            _selectedPatient = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<PatientDirectoryInfo> _filteredPatients()
    {
        var filteredPatients = _patients.Where(
            patient => patient.Surname.StartsWith(SearchText, StringComparison.CurrentCultureIgnoreCase)
        );
        return new ObservableCollection<PatientDirectoryInfo>(filteredPatients);
    }
}