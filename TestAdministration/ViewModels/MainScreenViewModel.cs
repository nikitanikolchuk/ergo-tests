using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace TestAdministration.ViewModels;

/// <summary>
/// The ViewModel that handles navigation after logging in.
/// </summary>
public partial class MainScreenViewModel : ViewModelBase
{
    private const string TextManualsLink = "https://rehabilitace.lf1.cuni.cz/publikacni-cinnost-uvod";
    private const string VideoManualsLink = "https://kurzy.lf1.cuni.cz/";

    [GeneratedRegex(@"\s+")]
    private static partial Regex WhitespaceRegex();

    // TODO: replace with actual path
    private readonly string _dataPath =
        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
        ?? throw new ArgumentException("Can't get exe directory");

    // TODO: replace with actual tester
    public string CurrentTester => "Jan Novák";

    public string CurrentTesterInitials
    {
        get
        {
            if (string.IsNullOrWhiteSpace(CurrentTester))
            {
                return "";
            }

            var names = WhitespaceRegex()
                .Replace(CurrentTester, " ")
                .Trim()
                .Split(" ");

            if (names.Length == 1)
            {
                return names[0].Length == 1
                    ? names[0][..1]
                    : names[0][..2];
            }

            return $"{names[0][0]}{names[1][0]}";
        }
    }

    public ICommand ResultsButtonCommand => new RelayCommand(_ =>
        Process.Start("explorer.exe", _dataPath)
    );

    public ICommand TextManualsButtonCommand => new RelayCommand(_ =>
        Process.Start(new ProcessStartInfo(TextManualsLink) { UseShellExecute = true })
    );

    public ICommand VideoManualsButtonCommand => new RelayCommand(_ =>
        Process.Start(new ProcessStartInfo(VideoManualsLink) { UseShellExecute = true })
    );
}