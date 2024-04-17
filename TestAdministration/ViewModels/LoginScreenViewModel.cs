using System.Windows.Input;
using Microsoft.Win32;
using TestAdministration.Models.Services;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace TestAdministration.ViewModels;

/// <summary>
/// A ViewModel for user and test data storage configuration.
/// </summary>
public class LoginScreenViewModel(
    LocalStorageService localStorageService,
    IContentDialogService contentDialogService
) : ViewModelBase
{
    private string _newSharePointTestDataPath = string.Empty;
    private string _newLocalTestDataPath = string.Empty;

    public string SharePointTestDataPath
    {
        get => localStorageService.SharePointTestDataPath;
        set
        {
            if (localStorageService.SharePointTestDataPath == value)
            {
                return;
            }

            localStorageService.SharePointTestDataPath = value;
            OnPropertyChanged();
        }
    }

    public string NewSharePointTestDataPath
    {
        get => _newSharePointTestDataPath;
        set
        {
            if (_newSharePointTestDataPath == value)
            {
                return;
            }

            // TODO: add validation

            _newSharePointTestDataPath = value;
            OnPropertyChanged();
        }
    }

    public string LocalTestDataPath
    {
        get => localStorageService.LocalTestDataPath;
        set
        {
            if (localStorageService.LocalTestDataPath == value)
            {
                return;
            }

            localStorageService.LocalTestDataPath = value;
            OnPropertyChanged();
        }
    }

    public string NewLocalTestDataPath
    {
        get => _newLocalTestDataPath;
        set
        {
            if (_newLocalTestDataPath == value)
            {
                return;
            }

            // TODO: add validation

            _newLocalTestDataPath = value;
            OnPropertyChanged();
        }
    }

    public ICommand OnOpenSharePointConfigDialogCommand => new RelayCommand(_onOpenSharePointConfigDialog);
    public ICommand OnOpenLocalConfigDialogCommand => new RelayCommand(_onOpenLocalConfigDialog);
    public ICommand OnOpenDirectoryCommand => new RelayCommand(_onOpenDirectory);

    private async void _onOpenSharePointConfigDialog(object? obj)
    {
        if (obj is not ContentDialog content)
        {
            throw new ArgumentException("Command parameter is not a ContentDialog");
        }

        var result = await contentDialogService.ShowAsync(content, CancellationToken.None);

        if (result == ContentDialogResult.Primary)
        {
            SharePointTestDataPath = NewSharePointTestDataPath;
        }

        NewSharePointTestDataPath = string.Empty;
    }

    private async void _onOpenLocalConfigDialog(object? obj)
    {
        if (obj is not ContentDialog content)
        {
            throw new ArgumentException("Command parameter is not a ContentDialog");
        }

        var result = await contentDialogService.ShowAsync(content, CancellationToken.None);

        if (result == ContentDialogResult.Primary)
        {
            LocalTestDataPath = NewLocalTestDataPath;
        }

        NewLocalTestDataPath = string.Empty;
    }

    private void _onOpenDirectory(object? obj)
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
}