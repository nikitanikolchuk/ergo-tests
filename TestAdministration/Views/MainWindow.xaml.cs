using System.Windows.Input;
using System.Windows.Media;
using TestAdministration.ViewModels;

namespace TestAdministration.Views;

public partial class MainWindow
{
    private const int NarrowWidth = 1920;
    private const int ShortHeight = 1080;

    public MainWindow(MainWindowViewModel mainWindowViewModel)
    {
        InitializeComponent();
        Loaded += (_, _) => _updateLayoutMode();
        SizeChanged += (_, _) => _updateLayoutMode();

        mainWindowViewModel.ContentDialogService.SetDialogHost(RootContentDialog);
        DataContext = mainWindowViewModel;
    }

    private void _onSpaceBarPressed(object sender, KeyEventArgs e) =>
        ((MainWindowViewModel)DataContext).OnSpaceBarPressed(sender, e);

    private void _updateLayoutMode()
    {
        if (DataContext is not MainWindowViewModel viewModel)
        {
            return;
        }

        var dpi = VisualTreeHelper.GetDpi(this);
        var pixelWidth = ActualWidth * dpi.DpiScaleX;
        var pixelHeight = ActualHeight * dpi.DpiScaleY;

        viewModel.LayoutState.IsCompactLayout = pixelWidth <= NarrowWidth || pixelHeight <= ShortHeight;
    }
}