using System.Windows.Media.Imaging;
using TestAdministration.Models.Services;
using Wpf.Ui.Controls;

namespace TestAdministration.ViewModels;

/// <summary>
/// A view model for displaying camera feed and managing video
/// recording.
/// </summary>
public class CameraFeedViewModel : ViewModelBase, IDisposable
{
    private readonly VideoRecorderService _videoRecorderService;

    public CameraFeedViewModel(VideoRecorderService videoRecorderService)
    {
        _videoRecorderService = videoRecorderService;
        _videoRecorderService.NewFrameAvailable += _onNewFrameAvailable;
        _videoRecorderService.RecordingTimeUpdated += _onRecordingTimeUpdated;
    }

    ~CameraFeedViewModel() => Dispose();

    public BitmapSource? CameraFeedImage
    {
        get;
        private set
        {
            field = value;
            OnPropertyChanged();
        }
    }

    public bool IsRecording => _videoRecorderService is { IsRecording: true, IsPaused: false };

    public string RecordingTime
    {
        get;
        private set
        {
            field = value;
            OnPropertyChanged();
        }
    } = "00:00";

    public async Task<bool> OnStartCamera()
    {
        var startSuccessful = _videoRecorderService.StartCamera();

        if (!startSuccessful)
        {
            var messageBox = new MessageBox
            {
                Title = "Chyba",
                Content = "Nahrávání se nepodařilo spustit. " +
                          "Zkontrolujte připojení kamery nebo v nastavení zvolte jinou.",
                CloseButtonText = "Zavřít"
            };

            await messageBox.ShowDialogAsync();
        }

        return startSuccessful;
    }

    public async Task OnPauseRecording()
    {
        if (!_videoRecorderService.IsRecording)
        {
            if (!_videoRecorderService.IsCameraRunning)
            {
                var startSuccessful = await OnStartCamera();
                if (!startSuccessful)
                {
                    return;
                }
            }

            _videoRecorderService.StartRecording();
        }
        else
        {
            _videoRecorderService.PauseRecording();
        }

        OnPropertyChanged(nameof(IsRecording));
    }

    public void OnStopRecording() => _videoRecorderService.StopCamera();

    private void _onNewFrameAvailable(BitmapSource bitmapSource) => CameraFeedImage = bitmapSource;

    public void Dispose()
    {
        _videoRecorderService.StopCamera();
        GC.SuppressFinalize(this);
    }

    private void _onRecordingTimeUpdated(TimeSpan time) => RecordingTime = time.ToString(@"mm\:ss");
}