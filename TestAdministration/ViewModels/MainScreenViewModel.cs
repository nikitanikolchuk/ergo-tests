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
    ConfigurationService configurationService,
    ITestStorage testStorage,
    TestingViewModelFactory testingViewModelFactory
) : ViewModelBase
{
    [GeneratedRegex(@"\s+")]
    private static partial Regex WhitespaceRegex();

    private string _contentHeader = string.Empty;
    private ViewModelBase _currentViewModel = new InitContentViewModel();

    public string CurrentUser => configurationService.CurrentUser;

    public string CurrentUserInitials
    {
        get
        {
            if (string.IsNullOrWhiteSpace(CurrentUser))
            {
                return string.Empty;
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
        set
        {
            _contentHeader = value;
            OnPropertyChanged();
        }
    }

    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel = value;
            OnPropertyChanged();
        }
    }

    public ICommand OnStartTestingCommand => new RelayCommand<TestType>(_onStartTesting);
    public ICommand OnOpenResultsCommand => new RelayCommand<object?>(_ => _onOpenResultsSection());
    public ICommand OnOpenTextManualsCommand => new RelayCommand<object?>(_ => _onOpenTextManuals());
    public ICommand OnOpenVideoManualsCommand => new RelayCommand<object?>(_ => _onOpenVideoManuals());
    public ICommand OnOpenSettingsCommand => new RelayCommand<object?>(_ => _onOpenSettingsCommand());

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

    private void _onOpenResultsSection()
    {
        ContentHeader = "Výsledky";
        CurrentViewModel = new ResultsStorageViewModel(testStorage.DataPath);
    }

    private void _onOpenTextManuals()
    {
        ContentHeader = "Textové manuály";
        CurrentViewModel = new TextManualsViewModel();
    }

    private void _onOpenVideoManuals()
    {
        ContentHeader = "Videomanuály";
        CurrentViewModel = new VideoManualsViewModel();
    }

    private void _onOpenSettingsCommand()
    {
        ContentHeader = "Nastavení";
        CurrentViewModel = new SettingsViewModel(configurationService);
    }
}