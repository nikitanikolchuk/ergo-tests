using System.Windows;
using TestAdministration.Models.Services;
using Wpf.Ui.Appearance;

namespace TestAdministration.ViewModels;

/// <summary>
/// A view model for setting application-wide visual preferences.
/// </summary>
public class SettingsViewModel(
    ConfigurationService configurationService
) : ViewModelBase
{
    public static List<int> FontSizeVariants => [14, 16, 18, 20];

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
}