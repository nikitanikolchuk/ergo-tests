using System.Text.RegularExpressions;
using System.Windows.Input;
using TestAdministration.Models.Data;
using TestAdministration.Models.Services;
using TestAdministration.Models.Storages;
using Wpf.Ui.Controls;
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
        var contentHeader = testType switch
        {
            TestType.Nhpt => "Devítikolíkový Test",
            TestType.Ppt => "Purdue Pegboard Test",
            TestType.Bbt => "Box and Block Test",
            _ => string.Empty
        };

        _navigate(contentHeader, () => testingViewModelFactory.Create(testType));
    }

    private void _onOpenResultsSection() =>
        _navigate("Výsledky", () => new ResultsStorageViewModel(testStorage.DataPath));

    private void _onOpenTextManuals() =>
        _navigate("Textové manuály", () => new TextManualsViewModel());

    private void _onOpenVideoManuals() =>
        _navigate("Videomanuály", () => new VideoManualsViewModel());

    private void _onOpenSettingsCommand() =>
        _navigate("Nastavení", () => new SettingsViewModel(configurationService));

    /// <summary>
    /// Navigates to another page if the user confirms it.
    /// View model creating delegate is used to avoid its
    /// creation in case user cancels the navigation request.
    /// </summary>
    private async void _navigate(string contentHeader, Func<ViewModelBase> createViewModel)
    {
        var navigationConfirmed = await _confirmNavigation();
        if (!navigationConfirmed)
        {
            return;
        }

        ContentHeader = contentHeader;
        CurrentViewModel = createViewModel();
    }

    private async Task<bool> _confirmNavigation()
    {
        if (CurrentViewModel is not TestingViewModel { IsBlockingNavigation: true })
        {
            return true;
        }

        var messageBox = new MessageBox
        {
            Title = "Upozornění",
            Content = "Opravdu chcete přejít na jinou stránku? Všechna neuložená data budou ztrácena",
            PrimaryButtonText = "Potvrdit",
            CloseButtonText = "Zrušit"
        };

        var messageBoxResult = await messageBox.ShowDialogAsync();
        return messageBoxResult == MessageBoxResult.Primary;
    }
}