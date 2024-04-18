using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Input;
using TestAdministration.Models.Services;
using Wpf.Ui.Input;

namespace TestAdministration.ViewModels;

/// <summary>
/// The ViewModel that handles navigation after logging in.
/// </summary>
public partial class MainScreenViewModel(
    UserService userService
) : ViewModelBase
{
    private const string TextManualsLink = "https://rehabilitace.lf1.cuni.cz/publikacni-cinnost-uvod";
    private const string VideoManualsLink = "https://kurzy.lf1.cuni.cz/";

    [GeneratedRegex(@"\s+")]
    private static partial Regex WhitespaceRegex();

    // TODO: replace with actual path
    private readonly string _dataPath =
        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
        ?? throw new ArgumentException("Can't get exe directory");

    public string CurrentUser => userService.CurrentUser ?? "";

    public string CurrentUserInitials
    {
        get
        {
            if (string.IsNullOrWhiteSpace(CurrentUser))
            {
                return "";
            }

            var names = WhitespaceRegex()
                .Replace(CurrentUser, " ")
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

    public ICommand ResultsButtonCommand => new RelayCommand<object?>(_ =>
        Process.Start("explorer.exe", _dataPath)
    );

    public ICommand TextManualsButtonCommand => new RelayCommand<object?>(_ =>
        Process.Start(new ProcessStartInfo(TextManualsLink) { UseShellExecute = true })
    );

    public ICommand VideoManualsButtonCommand => new RelayCommand<object?>(_ =>
        Process.Start(new ProcessStartInfo(VideoManualsLink) { UseShellExecute = true })
    );
}