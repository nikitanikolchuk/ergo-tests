using System.Windows.Media.Imaging;
using TestAdministration.Models.Services;

namespace TestAdministration.ViewModels;

public class CameraFeedViewModel : ViewModelBase
{
    private readonly CameraCaptureService _cameraCaptureService;

    private BitmapSource? _cameraFeedImage;

    public CameraFeedViewModel(CameraCaptureService cameraCaptureService)
    {
        _cameraCaptureService = cameraCaptureService;
        _cameraCaptureService.NewFrameAvailable += _onNewFrameAvailable;
        _cameraCaptureService.StartCamera();
    }

    public BitmapSource? CameraFeedImage
    {
        get => _cameraFeedImage;
        private set
        {
            _cameraFeedImage = value;
            OnPropertyChanged();
        }
    }

    private void _onNewFrameAvailable(BitmapSource bitmapSource) => CameraFeedImage = bitmapSource;
    ~CameraFeedViewModel() => _cameraCaptureService.StopCamera();
}