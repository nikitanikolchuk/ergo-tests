using TestAdministration.ViewModels;

namespace TestAdministration.Views;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();

        ((MainWindowViewModel)DataContext).ContentDialogService.SetDialogHost(RootContentDialog);
    }
}