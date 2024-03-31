using System.Diagnostics;
using System.Windows;

namespace TestAdministration.Views;

public partial class NavBar
{
    private const string TextManualsLink = "https://rehabilitace.lf1.cuni.cz/publikacni-cinnost-uvod";
    private const string VideoManualsLink = "https://kurzy.lf1.cuni.cz/";

    public NavBar()
    {
        InitializeComponent();
    }

    private void OnTextManualsClick(object sender, RoutedEventArgs e)
    {
        Process.Start(new ProcessStartInfo(TextManualsLink) { UseShellExecute = true });
    }

    private void OnVideoManualsClick(object sender, RoutedEventArgs e)
    {
        Process.Start(new ProcessStartInfo(VideoManualsLink) { UseShellExecute = true });
    }
}