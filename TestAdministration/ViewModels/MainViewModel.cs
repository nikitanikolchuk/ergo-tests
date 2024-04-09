using System.Windows.Input;

namespace TestAdministration.ViewModels;

/// <summary>
/// The main application viewmodel.
/// </summary>
public class MainViewModel(
    LoginScreenViewModel loginScreenScreenViewModel,
    MainScreenViewModel mainScreenViewModel
) : ViewModelBase
{
    private ViewModelBase _screenScreenViewModel = loginScreenScreenViewModel;

    /// <summary>
    /// View model with an associated view that fills the whole
    /// main window. 
    /// </summary>
    public ViewModelBase ScreenScreenViewModel
    {
        get => _screenScreenViewModel;
        private set
        {
            if (_screenScreenViewModel == value)
            {
                return;
            }

            _screenScreenViewModel = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Displays main screen using chosen login credentials.
    /// </summary>
    public ICommand DisplayMainScreenCommand =>
        new RelayCommand(
            _ => _displayMainScreen(),
            _ => ScreenScreenViewModel is not MainScreenViewModel
        );

    /// <summary>
    /// Logs out the current user and displays initial login page. 
    /// </summary>
    public ICommand DisplayLoginScreenCommand =>
        new RelayCommand(
            _ => _displayLoginScreen(),
            _ => ScreenScreenViewModel is not LoginScreenViewModel
        );

    private void _displayMainScreen()
    {
        // TODO: add login logic
        
        ScreenScreenViewModel = mainScreenViewModel;
    }

    private void _displayLoginScreen()
    {
        // TODO: add logout logic
        
        ScreenScreenViewModel = loginScreenScreenViewModel;
    }
}