using System.Windows;
using System.Windows.Input;
using TestAdministration.Views;

namespace TestAdministration.ViewModels;

public class LoginScreenViewModel : ViewModelBase
{
    public static ICommand OpenSharePointConfigWindow => new RelayCommand(_ =>
    {
        // TODO
    });

    public static ICommand OpenLocalConfigWindow => new RelayCommand(_ =>
    {
        var window = new LocalConfigWindow
        {
            Owner = Application.Current.MainWindow
        };
        window.ShowDialog();
    });
}