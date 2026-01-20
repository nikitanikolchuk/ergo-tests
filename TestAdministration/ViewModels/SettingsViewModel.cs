using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Threading;
using NAudio.Wave;
using OpenCvSharp;
using TestAdministration.Models.Services;
using Wpf.Ui;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;
using Wpf.Ui.Input;
using Window = System.Windows.Window;

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

            // Force frame/backdrop redraw (fixes theme switching)
            if (Application.Current.MainWindow is { } window)
            {
                window.Dispatcher.InvokeAsync(() =>
                {
                    WindowBackdrop.RemoveBackdrop(window);
                    WindowBackdrop.RemoveBackground(window);
                    WindowBackdrop.ApplyBackdrop(window, WindowBackdropType.Mica);
                    WindowBackdrop.RemoveTitlebarBackground(window);

                    window.ClearValue(Control.BackgroundProperty);

                    _forceNonClientRefresh(window);
                }, DispatcherPriority.ApplicationIdle);
            }

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

        var startSuccessful = await CameraFeedViewModel.OnStartCamera();
        if (!startSuccessful)
        {
            return;            
        }

        dialog.DataContext = this;
        _ = await contentDialogService.ShowAsync(dialog, CancellationToken.None);
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private static void _forceNonClientRefresh(Window w)
    {
        var hWnd = new WindowInteropHelper(w).Handle;
        if (hWnd == IntPtr.Zero)
        {
            return;
        }

        const uint SWP_NOSIZE = 0x0001;
        const uint SWP_NOMOVE = 0x0002;
        const uint SWP_NOZORDER = 0x0004;
        const uint SWP_FRAMECHANGED = 0x0020;

        const uint uFlags = SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_FRAMECHANGED;

        SetWindowPos(hWnd, IntPtr.Zero, 0, 0, 0, 0, uFlags);
    }

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SetWindowPos(
        IntPtr hWnd,
        IntPtr hWndInsertAfter,
        int x,
        int y,
        int cx,
        int cy,
        uint uFlags
    );
}