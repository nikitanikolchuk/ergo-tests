using System.Windows.Input;

namespace TestAdministration.ViewModels;

/// <summary>
/// The main application viewmodel.
/// </summary>
public class MainViewModel(
    LoginScreenViewModel loginScreenViewModel,
    MainScreenViewModel mainScreenViewModel
) : ViewModelBase
{
    private ViewModelBase _screenViewModel = loginScreenViewModel;

    /// <summary>
    /// View model with an associated view that fills the whole
    /// main window. 
    /// </summary>
    public ViewModelBase ScreenViewModel
    {
        get => _screenViewModel;
        private set
        {
            if (_screenViewModel == value)
            {
                return;
            }

            _screenViewModel = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Displays main screen using chosen login credentials.
    /// </summary>
    public ICommand DisplayMainScreenCommand =>
        new RelayCommand(
            _ => _displayMainScreen(),
            _ => ScreenViewModel is not MainScreenViewModel
        );

    /// <summary>
    /// Logs out the current user and displays initial login page. 
    /// </summary>
    public ICommand DisplayLoginScreenCommand =>
        new RelayCommand(
            _ => _displayLoginScreen(),
            _ => ScreenViewModel is not LoginScreenViewModel
        );

    private void _displayMainScreen()
    {
        // TODO: add login logic
        
        ScreenViewModel = mainScreenViewModel;
    }

    private void _displayLoginScreen()
    {
        // TODO: add logout logic
        
        ScreenViewModel = loginScreenViewModel;
    }
}