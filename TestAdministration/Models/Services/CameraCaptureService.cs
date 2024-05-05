using System.Windows.Media.Imaging;
using OpenCvSharp;
using OpenCvSharp.WpfExtensions;

namespace TestAdministration.Models.Services;

/// <summary>
/// A class for displaying default camera's feed.
/// </summary>
public class CameraCaptureService
{
    private const int UpdateTimeoutMilliseconds = 33;

    private VideoCapture? _capture;
    private bool _isCameraRunning;
    private CancellationTokenSource? _cancellationTokenSource;

    public event Action<BitmapSource>? NewFrameAvailable;

    public void StartCamera()
    {
        if (_isCameraRunning)
        {
            return;
        }

        _capture = new VideoCapture(0);
        _isCameraRunning = true;
        _cancellationTokenSource = new CancellationTokenSource();

        Task.Run(() =>
            {
                while (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    using var mat = _capture.RetrieveMat();
                    if (mat.Empty())
                    {
                        continue;
                    }

                    var bitmapSource = mat.ToBitmapSource();
                    bitmapSource.Freeze();
                    NewFrameAvailable?.Invoke(bitmapSource);

                    Thread.Sleep(UpdateTimeoutMilliseconds);
                }
            },
            _cancellationTokenSource.Token
        );
    }

    public void StopCamera()
    {
        if (!_isCameraRunning)
        {
            return;
        }

        _cancellationTokenSource?.Cancel();
        _isCameraRunning = false;
        _capture?.Release();
    }
}