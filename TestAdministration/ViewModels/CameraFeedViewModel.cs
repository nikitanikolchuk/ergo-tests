using System.Windows.Media.Imaging;
using TestAdministration.Models.Services;

namespace TestAdministration.ViewModels;

/// <summary>
/// A view model for displaying camera feed and managing video
/// recording.
/// </summary>
public class CameraFeedViewModel : ViewModelBase, IDisposable
{
    private readonly VideoRecorderService _videoRecorderService;

    private BitmapSource? _cameraFeedImage;
    private string _recordingTime = "00:00";

    public CameraFeedViewModel(VideoRecorderService videoRecorderService)
    {
        _videoRecorderService = videoRecorderService;
        _videoRecorderService.NewFrameAvailable += _onNewFrameAvailable;
        _videoRecorderService.RecordingTimeUpdated += _onRecordingTimeUpdated;
    }

    ~CameraFeedViewModel() => Dispose();

    public BitmapSource? CameraFeedImage
    {
        get => _cameraFeedImage;
        private set
        {
            _cameraFeedImage = value;
            OnPropertyChanged();
        }
    }

    public bool IsRecording => _videoRecorderService is { IsRecording: true, IsPaused: false };

    public string RecordingTime
    {
        get => _recordingTime;
        private set
        {
            _recordingTime = value;
            OnPropertyChanged();
        }
    }

    public void OnStartCamera() => _videoRecorderService.StartCamera();

    public void OnPauseRecording()
    {
        if (!_videoRecorderService.IsRecording)
        {
            if (!_videoRecorderService.IsCameraRunning)
            {
                _videoRecorderService.StartCamera();
            }

            _videoRecorderService.StartRecording();
        }
        else
        {
            _videoRecorderService.PauseRecording();
        }

        OnPropertyChanged(nameof(IsRecording));
    }

    public void OnStopRecording() => _videoRecorderService.StopRecording();

    private void _onNewFrameAvailable(BitmapSource bitmapSource) => CameraFeedImage = bitmapSource;
    private void _onRecordingTimeUpdated(TimeSpan time) => RecordingTime = time.ToString(@"mm\:ss");

    public void Dispose()
    {
        _videoRecorderService.StopCamera();
        GC.SuppressFinalize(this);
    }
}