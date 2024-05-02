using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Input;
using TestAdministration.Models.Data;
using TestAdministration.Models.Services;
using TestAdministration.Models.Storages;
using Wpf.Ui.Input;

namespace TestAdministration.ViewModels;

/// <summary>
/// The ViewModel that handles navigation after logging in.
/// </summary>
public partial class MainScreenViewModel(
    UserService userService,
    ITestStorage testStorage,
    InitContentViewModel initContentViewModel,
    SettingsViewModel settingsViewModel,
    TestingViewModelFactory testingViewModelFactory
) : ViewModelBase
{
    private const string TextManualsLink = "https://rehabilitace.lf1.cuni.cz/publikacni-cinnost-uvod";
    private const string VideoManualsLink = "https://kurzy.lf1.cuni.cz/";

    [GeneratedRegex(@"\s+")]
    private static partial Regex WhitespaceRegex();

    private string _contentHeader = string.Empty;
    private ViewModelBase _currentViewModel = initContentViewModel;

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

    public string ContentHeader
    {
        get => _contentHeader;
        private set
        {
            _contentHeader = value;
            OnPropertyChanged();
        }
    }

    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        private set
        {
            _currentViewModel = value;
            OnPropertyChanged();
        }
    }

    public ICommand OnStartTestingCommand => new RelayCommand<TestType>(_onStartTesting);

    public ICommand OnOpenResultsCommand => new RelayCommand<object?>(_ =>
        Process.Start("explorer.exe", testStorage.DataPath)
    );

    public static ICommand OnOpenTextManualsCommand => new RelayCommand<object?>(_ =>
        Process.Start(new ProcessStartInfo(TextManualsLink) { UseShellExecute = true })
    );

    public static ICommand OnOpenVideoManualsCommand => new RelayCommand<object?>(_ =>
        Process.Start(new ProcessStartInfo(VideoManualsLink) { UseShellExecute = true })
    );

    public ICommand OnOpenSettingsCommand => new RelayCommand<object?>(_ =>
        CurrentViewModel = settingsViewModel
    );

    private void _onStartTesting(TestType testType)
    {
        ContentHeader = testType switch
        {
            TestType.Nhpt => "Devítikolíkový Test",
            TestType.Ppt => "Purdue Pegboard Test",
            TestType.Bbt => "Box and Block Test",
            _ => string.Empty
        };

        var testingViewModel = testingViewModelFactory.Create(testType);
        CurrentViewModel = testingViewModel;
    }
}