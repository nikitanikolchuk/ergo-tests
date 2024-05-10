using System.IO;
using System.Windows.Media.Imaging;
using FFMpegCore;
using NAudio.Wave;
using OpenCvSharp;
using OpenCvSharp.WpfExtensions;

namespace TestAdministration.Models.Services;

/// <summary>
/// A class for displaying default camera's feed and recording video and audio.
/// </summary>
public class VideoRecorderService
{
    private const string TempRecordingFileName = "TempRecording.mp4";
    private const string TempVideoFileName = "TempVideo.mp4";
    private const string TempAudioFileName = "TempAudio.wav";
    private const int UpdateTimeoutMilliseconds = 30;
    private const int Mp4Tag = 0x7634706d;
    private const int AudioSampleRate = 44100;

    public static readonly string TempRecordingFilePath = _getFilePath(TempRecordingFileName);
    private static readonly string TempVideoFilePath = _getFilePath(TempVideoFileName);
    private static readonly string TempAudioFilePath = _getFilePath(TempAudioFileName);

    private VideoCapture? _capture;
    private VideoWriter? _writer;
    private WaveIn? _waveIn;
    private WaveFileWriter? _waveFileWriter;
    private bool _isCameraRunning;
    private TimeSpan _recordingTime = TimeSpan.Zero;
    private CancellationTokenSource? _cancellationTokenSource;

    static VideoRecorderService()
    {
        var binaryFolder = Path.Combine(AppContext.BaseDirectory, "Resources", "ffmpeg", "bin");
        GlobalFFOptions.Configure(options => options.BinaryFolder = binaryFolder);
    }

    public event Action<BitmapSource>? NewFrameAvailable;
    public event Action<TimeSpan>? RecordingTimeUpdated;

    public bool IsRecording { get; private set; }
    public bool IsPaused { get; private set; }

    public void StartCamera()
    {
        if (_isCameraRunning)
        {
            StopRecording();
            _deleteTempRecording();
            _deleteTempMediaFiles();
            return;
        }

        _deleteTempRecording();
        _deleteTempMediaFiles();

        _capture = new VideoCapture();
        // TODO: add device selection
        if (!_capture.Open(0))
        {
            return;
        }

        _isCameraRunning = true;
        _cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = _cancellationTokenSource.Token;

        Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
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
            cancellationToken
        );
    }

    public void StopCamera()
    {
        if (!_isCameraRunning)
        {
            _deleteTempRecording();
            _deleteTempMediaFiles();
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
            TempVideoFilePath,
            new FourCC(Mp4Tag),
            _capture.Fps,
            new Size(_capture.FrameWidth, _capture.FrameHeight)
        );

        _waveIn = new WaveIn
        {
            WaveFormat = new WaveFormat(AudioSampleRate, 1)
        };
        _waveFileWriter = new WaveFileWriter(TempAudioFilePath, _waveIn.WaveFormat);
        // TODO: add audio synchronization
        _waveIn.DataAvailable += (_, e) =>
        {
            if (_waveFileWriter == null || IsPaused)
            {
                return;
            }

            _waveFileWriter.Write(e.Buffer, 0, e.BytesRecorded);
            _waveFileWriter.Flush();
        };
        _waveIn.StartRecording();

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

        _waveIn?.StopRecording();
        _waveIn?.Dispose();
        _waveIn = null;
        _waveFileWriter?.Dispose();
        _waveFileWriter = null;

        _mergeMediaFiles();
    }

    private static string _getFilePath(string fileName)
    {
        var exePath = AppContext.BaseDirectory;
        var exeDirectoryPath = Path.GetDirectoryName(exePath)
                               ?? throw new ArgumentException("Can't get exe directory");
        return Path.Combine(exeDirectoryPath, fileName);
    }

    private static void _deleteTempRecording()
    {
        if (File.Exists(TempRecordingFilePath))
        {
            File.Delete(TempRecordingFilePath);
        }
    }

    private static void _deleteTempMediaFiles()
    {
        if (File.Exists(TempVideoFilePath))
        {
            File.Delete(TempVideoFilePath);
        }

        if (File.Exists(TempAudioFilePath))
        {
            File.Delete(TempAudioFilePath);
        }
    }

    private static void _mergeMediaFiles()
    {
        FFMpeg.ReplaceAudio(TempVideoFilePath, TempAudioFilePath, TempRecordingFilePath);
        _deleteTempMediaFiles();
    }
}