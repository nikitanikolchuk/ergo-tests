using TestAdministration.Models.Services;

namespace TestAdministration.ViewModels;

public class LocalConfigWindowViewModel(
    LocalStorageService localStorageService
) : ViewModelBase
{
    private string _newTestDataPath = string.Empty;

    public string CurrentTestDataPath
    {
        get => localStorageService.LocalTestDataPath;
        set
        {
            if (localStorageService.LocalTestDataPath == value)
            {
                return;
            }

            localStorageService.LocalTestDataPath = value;
            OnPropertyChanged();
        }
    }

    public string NewTestDataPath
    {
        get => _newTestDataPath;
        set
        {
            if (_newTestDataPath == value)
            {
                return;
            }

            _newTestDataPath = value;
            OnPropertyChanged();
        }
    }
}