using System.Windows.Input;
using TestAdministration.ViewModels;

namespace TestAdministration.Views;

public partial class MainWindow
{
    public MainWindow(MainWindowViewModel mainWindowViewModel)
    {
        InitializeComponent();

        mainWindowViewModel.ContentDialogService.SetDialogHost(RootContentDialog);
        DataContext = mainWindowViewModel;
    }

    private void _onSpaceBarPressed(object sender, KeyEventArgs e) =>
        ((MainWindowViewModel)DataContext).OnSpaceBarPressed(sender, e);
}