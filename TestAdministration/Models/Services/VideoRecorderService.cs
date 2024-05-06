using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;
using OpenCvSharp;
using OpenCvSharp.WpfExtensions;

namespace TestAdministration.Models.Services;

/// <summary>
/// A class for displaying default camera's feed.
/// </summary>
public class VideoRecorderService
{
    private const string TempFileName = "TempRecording.mp4";
    private const int UpdateTimeoutMilliseconds = 30;
    private const int Mp4Tag = 0x7634706d;

    public static readonly string TempFilePath = _getFilePath();

    private VideoCapture? _capture;
    private VideoWriter? _writer;
    private bool _isCameraRunning;
    private TimeSpan _recordingTime = TimeSpan.Zero;
    private CancellationTokenSource? _cancellationTokenSource;

    public event Action<BitmapSource>? NewFrameAvailable;
    public event Action<TimeSpan>? RecordingTimeUpdated;

    public bool IsRecording { get; private set; }
    public bool IsPaused { get; private set; }

    public void StartCamera()
    {
        if (_isCameraRunning)
        {
            StopRecording();
            _deleteTempFile();
            return;
        }

        _deleteTempFile();

        _capture = new VideoCapture();
        if (!_capture.Open(0))
        {
            return;
        }

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

                    if (IsRecording && !IsPaused)
                    {
                        _writer?.Write(mat);
                        _recordingTime += TimeSpan.FromMilliseconds(UpdateTimeoutMilliseconds);
                        RecordingTimeUpdated?.Invoke(_recordingTime);
                    }

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
            _deleteTempFile();
            return;
        }

        _cancellationTokenSource?.Cancel();
        _isCameraRunning = false;
        StopRecording();
        _capture?.Release();
    }

    public void StartRecording()
    {
        if (IsRecording || _capture is null)
        {
            return;
        }

        _writer = new VideoWriter(
            TempFilePath,
            new FourCC(Mp4Tag),
            _capture.Fps,
            new Size(_capture.FrameWidth, _capture.FrameHeight)
        );

        _recordingTime = TimeSpan.Zero;
        IsRecording = true;
        IsPaused = false;
    }

    public void PauseRecording() => IsPaused = !IsPaused;

    public void StopRecording()
    {
        if (!IsRecording)
        {
            return;
        }

        IsRecording = false;
        _writer?.Release();
    }

    private static string _getFilePath()
    {
        var exePath = AppContext.BaseDirectory;
        var exeDirectoryPath = Path.GetDirectoryName(exePath)
                               ?? throw new ArgumentException("Can't get exe directory");
        return Path.Combine(exeDirectoryPath, TempFileName);
    }

    private static void _deleteTempFile()
    {
        if (File.Exists(TempFilePath))
        {
            File.Delete(TempFilePath);
        }
    }
}