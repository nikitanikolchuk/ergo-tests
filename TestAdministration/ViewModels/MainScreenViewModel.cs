using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using TestAdministration.Models.Data;
using TestAdministration.Models.Services;
using TestAdministration.Models.Storages;
using TestAdministration.ViewModels.Testing;
using Wpf.Ui.Input;
using MessageBox = Wpf.Ui.Controls.MessageBox;
using MessageBoxResult = Wpf.Ui.Controls.MessageBoxResult;

namespace TestAdministration.ViewModels;

/// <summary>
/// The ViewModel that handles navigation after logging in.
/// </summary>
public partial class MainScreenViewModel(
    ConfigurationService configurationService,
    AudioInstructionService audioInstructionService,
    VideoRecorderService videoRecorderService,
    ITestStorage testStorage,
    TestingViewModelFactory testingViewModelFactory,
    SettingsViewModelFactory settingsViewModelFactory,
    LayoutStateViewModel layoutState
) : ViewModelBase
{
    [GeneratedRegex(@"\s+")]
    private static partial Regex WhitespaceRegex();

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

    public bool IsNavPaneOpen
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    } = true;

    public string ContentHeader
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    } = string.Empty;

    public Visibility ContentHeaderVisibility
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    } = Visibility.Visible;

    public ViewModelBase CurrentViewModel
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    } = new InitContentViewModel();

    public ICommand OnStartTestingCommand => new RelayCommand<TestType>(_onStartTesting);
    public ICommand OnOpenResultsCommand => new RelayCommand<object?>(_ => _onOpenResultsSection());
    public ICommand OnOpenTextManualsCommand => new RelayCommand<object?>(_ => _onOpenTextManuals());
    public ICommand OnOpenVideoManualsCommand => new RelayCommand<object?>(_ => _onOpenVideoManuals());
    public ICommand OnOpenAppManualCommand => new RelayCommand<object?>(_ => _onOpenAppManual());
    public ICommand OnOpenSettingsCommand => new RelayCommand<object?>(_ => _onOpenSettings());

    private void _onStartTesting(TestType testType)
    {
        var contentHeader = testType switch
        {
            TestType.Nhpt => "Devítikolíkový Test",
            TestType.Ppt => "Purdue Pegboard Test",
            TestType.Bbt => "Box and Block Test",
            _ => string.Empty
        };

        var setIsNavPaneOpen = (bool isNavPaneOpen) => { IsNavPaneOpen = isNavPaneOpen; };
        var setHeaderVisibility = (Visibility v) => { ContentHeaderVisibility = v; };
        _navigate(
            contentHeader,
            () => testingViewModelFactory.Create(testType, setIsNavPaneOpen, setHeaderVisibility)
        );
    }

    private void _onOpenResultsSection() =>
        _navigate("Výsledky", () => new ResultsStorageViewModel(testStorage.DataPath));

    private void _onOpenAppManual() =>
        _navigate("Návod k použití", () => new AppManualViewModel(layoutState));

    private void _onOpenTextManuals() =>
        _navigate("Textové manuály", () => new TextManualsViewModel());

    private void _onOpenVideoManuals() =>
        _navigate("Videomanuály", () => new VideoManualsViewModel());

    private void _onOpenSettings() =>
        _navigate("Nastavení", settingsViewModelFactory.Create);

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

        audioInstructionService.Stop();
        videoRecorderService.StopCamera();
        CurrentViewModel = createViewModel();
        ContentHeader = contentHeader;
        ContentHeaderVisibility = Visibility.Visible;
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