using System.Collections.Immutable;
using System.Windows.Input;
using Microsoft.Win32;
using TestAdministration.Models.Services;
using Wpf.Ui;
using Wpf.Ui.Controls;
using Wpf.Ui.Input;

namespace TestAdministration.ViewModels;

/// <summary>
/// A ViewModel for user and test data storage configuration.
/// </summary>
public class LoginScreenViewModel(
    ConfigurationService configurationService,
    UserService userService,
    IContentDialogService contentDialogService
) : ViewModelBase
{
    private string _newSharePointTestDataPath = string.Empty;
    private string _newLocalTestDataPath = string.Empty;
    private string _newUser = string.Empty;
    private string _userToDelete = string.Empty;

    public string SharePointTestDataPath
    {
        get => configurationService.SharePointTestDataPath;
        set
        {
            configurationService.SharePointTestDataPath = value;
            OnPropertyChanged();
        }
    }

    public string NewSharePointTestDataPath
    {
        get => _newSharePointTestDataPath;
        set
        {
            // TODO: add validation

            _newSharePointTestDataPath = value;
            OnPropertyChanged();
        }
    }

    public string LocalTestDataPath
    {
        get => configurationService.LocalTestDataPath;
        set
        {
            configurationService.LocalTestDataPath = value;
            OnPropertyChanged();
        }
    }

    public string NewLocalTestDataPath
    {
        get => _newLocalTestDataPath;
        set
        {
            _newLocalTestDataPath = value;
            OnPropertyChanged();
        }
    }

    public ImmutableList<string> Users
    {
        get => configurationService.LocalUsers;
        private set
        {
            configurationService.LocalUsers = value;
            OnPropertyChanged();
        }
    }

    public string? CurrentUser
    {
        get => userService.CurrentUser;
        set
        {
            userService.CurrentUser = value;
            OnPropertyChanged();
        }
    }

    public string NewUser
    {
        get => _newUser;
        set
        {
            _newUser = value;
            OnPropertyChanged();
        }
    }

    public string UserToDelete
    {
        get => _userToDelete;
        set
        {
            _userToDelete = value;
            OnPropertyChanged();
        }
    }

    public ICommand OnOpenSharePointConfigDialogCommand =>
        new RelayCommand<ContentDialog>(_onOpenSharePointConfigDialog);

    public ICommand OnOpenLocalConfigDialogCommand =>
        new RelayCommand<ContentDialog>(_onOpenLocalConfigDialog);

    public ICommand OnOpenDirectoryCommand =>
        new RelayCommand<object?>(_ => _onOpenDirectory());

    public ICommand OnOpenAddUserDialogCommand =>
        new RelayCommand<ContentDialog>(_onOpenAddUserDialog);

    public ICommand OnOpenDeleteUserDialogCommand =>
        new RelayCommand<ContentDialog>(_onOpenDeleteUserDialog);

    private async void _onOpenSharePointConfigDialog(ContentDialog? content)
    {
        if (content is null)
        {
            throw new ArgumentException("Content is null");
        }

        content.DataContext = this;
        var result = await contentDialogService.ShowAsync(content, CancellationToken.None);

        if (result == ContentDialogResult.Primary)
        {
            SharePointTestDataPath = NewSharePointTestDataPath;
        }

        NewSharePointTestDataPath = string.Empty;
    }

    private async void _onOpenLocalConfigDialog(ContentDialog? content)
    {
        if (content is null)
        {
            throw new ArgumentException("Content is null");
        }

        content.DataContext = this;
        var result = await contentDialogService.ShowAsync(content, CancellationToken.None);

        if (result == ContentDialogResult.Primary)
        {
            LocalTestDataPath = NewLocalTestDataPath;
        }

        NewLocalTestDataPath = string.Empty;
    }

    private void _onOpenDirectory()
    {
        OpenFolderDialog openFolderDialog = new()
        {
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        };

        if (openFolderDialog.ShowDialog() != true)
        {
            return;
        }

        if (openFolderDialog.FolderNames.Length == 0)
        {
            return;
        }

        NewLocalTestDataPath = openFolderDialog.FolderNames.First();
    }

    private async void _onOpenAddUserDialog(ContentDialog? content)
    {
        if (content is null)
        {
            throw new ArgumentException("Content is null");
        }

        content.DataContext = this;
        var result = await contentDialogService.ShowAsync(content, CancellationToken.None);

        if (result == ContentDialogResult.Primary)
        {
            Users = Users.Add(NewUser);
        }

        NewUser = string.Empty;
    }

    private async void _onOpenDeleteUserDialog(ContentDialog? content)
    {
        if (content is null)
        {
            throw new ArgumentException("Content is null");
        }

        content.DataContext = this;
        var result = await contentDialogService.ShowAsync(content, CancellationToken.None);

        if (result == ContentDialogResult.Primary)
        {
            Users = Users.Remove(UserToDelete);
        }

        UserToDelete = string.Empty;
    }
}