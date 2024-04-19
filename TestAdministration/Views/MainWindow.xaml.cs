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
}