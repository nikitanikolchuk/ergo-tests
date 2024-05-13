using System.Windows;
using System.Windows.Input;
using TestAdministration.Models.Services;
using Wpf.Ui;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;
using Wpf.Ui.Input;

namespace TestAdministration.ViewModels;

/// <summary>
/// A view model for setting application-wide visual preferences.
/// </summary>
public class SettingsViewModel(
    IContentDialogService contentDialogService,
    ConfigurationService configurationService,
    VideoRecorderService videoRecorderService
) : ViewModelBase
{
    public static List<int> FontSizeVariants => [12, 14, 16, 18, 20];

    public int FontSize
    {
        get => configurationService.FontSize;
        set
        {
            configurationService.FontSize = value;
            Application.Current.Resources["BaseFontSize"] = (double)value;
            Application.Current.Resources["ControlContentThemeFontSize"] = (double)value;
            OnPropertyChanged();
        }
    }

    public bool IsDarkModeSet
    {
        get => configurationService.ApplicationTheme == ApplicationTheme.Dark;
        set
        {
            var theme = value ? ApplicationTheme.Dark : ApplicationTheme.Light;
            configurationService.ApplicationTheme = theme;
            ApplicationThemeManager.Apply(theme);
            OnPropertyChanged();
        }
    }

    public CameraFeedViewModel? CameraFeedViewModel { get; private set; }

    public ICommand OnOpenCameraFeedDialog => new RelayCommand<ContentDialog>(_onOpenCameraFeedDialog);

    private async void _onOpenCameraFeedDialog(ContentDialog? dialog)
    {
        if (dialog is null)
        {
            throw new ArgumentException("Content is null");
        }

        using var cameraFeedViewModel = new CameraFeedViewModel(videoRecorderService);
        CameraFeedViewModel = cameraFeedViewModel;
        dialog.DataContext = this;
        _ = await contentDialogService.ShowAsync(dialog, CancellationToken.None);
    }
}