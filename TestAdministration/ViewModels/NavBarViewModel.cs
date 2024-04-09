using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Input;

namespace TestAdministration.ViewModels;

/// <summary>
/// View model for main navigation panel.
/// </summary>
public class NavBarViewModel : ViewModelBase
{
    private const string TextManualsLink = "https://rehabilitace.lf1.cuni.cz/publikacni-cinnost-uvod";
    private const string VideoManualsLink = "https://kurzy.lf1.cuni.cz/";

    // TODO: replace with actual path
    private readonly string _dataPath =
        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
        ?? throw new ArgumentException("Can't get exe directory");

    // TODO: replace with actual tester's name
    private readonly string _currentTesterName = "Jan";

    // TODO: replace with actual tester's surname
    private readonly string _currentTesterSurname = "Novák";

    public string CurrentTester => $"{_currentTesterName} {_currentTesterSurname}";
    public string CurrentTesterInitials => $"{_currentTesterName[0]}{_currentTesterSurname[0]}";

    public ICommand ResultsButtonCommand => new RelayCommand(_ => _onResultsClick());
    public ICommand TextManualsButtonCommand => new RelayCommand(_ => _onTextManualsClick());
    public ICommand VideoManualsButtonCommand => new RelayCommand(_ => _onVideoManualsClick());

    private void _onResultsClick()
    {
        Process.Start("explorer.exe", _dataPath);
    }

    private static void _onTextManualsClick()
    {
        Process.Start(new ProcessStartInfo(TextManualsLink) { UseShellExecute = true });
    }

    private static void _onVideoManualsClick()
    {
        Process.Start(new ProcessStartInfo(VideoManualsLink) { UseShellExecute = true });
    }
}