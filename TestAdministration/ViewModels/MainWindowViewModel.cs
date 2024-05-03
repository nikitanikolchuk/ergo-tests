using System.Windows.Input;
using Wpf.Ui;
using Wpf.Ui.Controls;
using Wpf.Ui.Input;
using static System.String;

namespace TestAdministration.ViewModels;

/// <summary>
/// The main application viewmodel.
/// </summary>
public class MainWindowViewModel(
    IContentDialogService contentDialogService,
    LoginScreenViewModel loginScreenViewModel,
    MainScreenViewModel mainScreenViewModel
) : ViewModelBase
{
    private ViewModelBase _screenViewModel = loginScreenViewModel;

    public IContentDialogService ContentDialogService => contentDialogService;

    /// <summary>
    /// View model with an associated view that fills the whole
    /// main window. 
    /// </summary>
    public ViewModelBase ScreenViewModel
    {
        get => _screenViewModel;
        private set
        {
            _screenViewModel = value;
            OnPropertyChanged();
        }
    }

    public ICommand OnDisplaySharePointMainScreenCommand => new RelayCommand<object?>(
        _ => _onDisplaySharePointMainScreen()
    );

    public ICommand OnDisplayLocalMainScreenCommand => new RelayCommand<object?>(
        _ => _onDisplayLocalMainScreen()
    );

    public ICommand OnDisplayLoginScreenCommand => new RelayCommand<object?>(
        _ => _onDisplayLoginScreen()
    );

    private void _onDisplaySharePointMainScreen()
    {
        // TODO: add validation

        ScreenViewModel = mainScreenViewModel;
    }

    private async void _onDisplayLocalMainScreen()
    {
        if (IsNullOrWhiteSpace(loginScreenViewModel.CurrentUser))
        {
            var messageBox = new MessageBox
            {
                Title = "Chyba",
                Content = "Nevybral(a) jste uživatelský účet",
                CloseButtonText = "Zavřít"
            };

            await messageBox.ShowDialogAsync();
            return;
        }

        if (IsNullOrWhiteSpace(loginScreenViewModel.LocalTestDataPath))
        {
            var messageBox = new MessageBox
            {
                Title = "Chyba",
                Content = "Nevybral(a) jste adresář pro uložení dat",
                CloseButtonText = "Zavřít"
            };

            await messageBox.ShowDialogAsync();
            return;
        }

        ScreenViewModel = mainScreenViewModel;
    }

    private void _onDisplayLoginScreen()
    {
        mainScreenViewModel.ContentHeader = Empty;
        mainScreenViewModel.CurrentViewModel = new InitContentViewModel();
        ScreenViewModel = loginScreenViewModel;
    }
}