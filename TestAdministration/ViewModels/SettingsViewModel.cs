using System.Windows;
using System.Windows.Input;
using NAudio.Wave;
using OpenCvSharp;
using TestAdministration.Models.Services;
using Wpf.Ui;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;
using Wpf.Ui.Input;

namespace TestAdministration.ViewModels;

/// <summary>
/// A view model for setting application-wide visual preferences.
/// </summary>
public class SettingsViewModel(
    IContentDialogService contentDialogService,
    ConfigurationService configurationService,
    VideoRecorderService videoRecorderService
) : ViewModelBase
{
    public static List<int> FontSizeVariants => [12, 14, 16, 18, 20];

    public int FontSize
    {
        get => configurationService.FontSize;
        set
        {
            configurationService.FontSize = value;
            Application.Current.Resources["BaseFontSize"] = (double)value;
            Application.Current.Resources["ControlContentThemeFontSize"] = (double)value;
            OnPropertyChanged();
        }
    }

    public bool IsDarkModeSet
    {
        get => configurationService.ApplicationTheme == ApplicationTheme.Dark;
        set
        {
            var theme = value ? ApplicationTheme.Dark : ApplicationTheme.Light;
            configurationService.ApplicationTheme = theme;
            ApplicationThemeManager.Apply(theme);
            OnPropertyChanged();
        }
    }

    public List<int> CameraDeviceIds { get; } = _getCameraDeviceIds();

    public int CameraId
    {
        get => configurationService.CameraId;
        set
        {
            videoRecorderService.StopCamera();
            configurationService.CameraId = value;
            OnPropertyChanged();
        }
    }

    public CameraFeedViewModel? CameraFeedViewModel { get; private set; }

    public List<string> MicrophoneNames { get; } = _getMicrophoneDeviceNames();

    public string MicrophoneName =>
        MicrophoneId >= 0 && MicrophoneId < MicrophoneNames.Count
            ? MicrophoneNames[MicrophoneId]
            : string.Empty;

    public int MicrophoneId
    {
        get => configurationService.MicrophoneId;
        set
        {
            configurationService.MicrophoneId = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(MicrophoneName));
        }
    }

    public ICommand OnOpenCameraFeedDialog => new RelayCommand<ContentDialog>(_onOpenCameraFeedDialog);

    private static List<int> _getCameraDeviceIds()
    {
        var ids = new List<int>();
        for (var i = 0; i < 10; i++)
        {
            using var capture = new VideoCapture();
            if (!capture.Open(i))
            {
                continue;
            }

            ids.Add(i);
            capture.Release();
        }

        return ids;
    }

    private static List<string> _getMicrophoneDeviceNames()
    {
        var names = new List<string>();
        for (var i = 0; i < WaveIn.DeviceCount; i++)
        {
            names.Add(WaveIn.GetCapabilities(i).ProductName);
        }

        return names;
    }

    private async void _onOpenCameraFeedDialog(ContentDialog? dialog)
    {
        if (dialog is null)
        {
            throw new ArgumentException("Content is null");
        }

        using var cameraFeedViewModel = new CameraFeedViewModel(videoRecorderService);
        CameraFeedViewModel = cameraFeedViewModel;
        CameraFeedViewModel.OnStartCamera();
        dialog.DataContext = this;
        _ = await contentDialogService.ShowAsync(dialog, CancellationToken.None);
    }
}