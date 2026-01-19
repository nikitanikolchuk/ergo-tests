using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using TestAdministration.Models.Data;
using TestAdministration.Models.Services;
using Wpf.Ui.Controls;
using Wpf.Ui.Input;

namespace TestAdministration.ViewModels.Testing.Instructions;

/// <summary>
/// A view model for an audio player for instructions.
/// </summary>
public class InstructionPlayerViewModel : ViewModelBase
{
    private readonly AudioInstructionService _audioService;
    private readonly string _audioFilePath;
    private readonly MediaPlayer _mediaPlayer;
    private readonly InstructionPlayerViewModel? _nextPlayer;

    private bool _audioOpened;

    public InstructionPlayerViewModel(
        AudioInstructionService audioService,
        string audioFilePath,
        InstructionPlayerViewModel? nextPlayer
    )
    {
        _audioService = audioService;
        _audioFilePath = audioFilePath;
        _mediaPlayer = new MediaPlayer();
        _nextPlayer = nextPlayer;

        _mediaPlayer.MediaEnded += _onAudioEnded;
        _mediaPlayer.MediaFailed += _onAudioFailed;

        AudioPlayer = new AudioPlayer(_onResume, _onPause, _onStop);

        var timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(1)
        };
        timer.Tick += _timerTick;
        timer.Start();
    }

    /// <summary>
    /// Occurs after a direct interaction with the audio player.
    /// </summary>
    public event Action? OnPlayStateChanged;

    public double ListenedPercentage
    {
        get => DurationSeconds != 0
            ? _mediaPlayer.Position.TotalSeconds / DurationSeconds
            : 0;
        set
        {
            _mediaPlayer.Position = TimeSpan.FromSeconds(DurationSeconds * value);
            OnPropertyChanged();
        }
    }

    public bool IsPlaying
    {
        get;
        private set
        {
            field = value;
            OnPropertyChanged();
        }
    }

    public AudioPlayer AudioPlayer { get; }
    public ICommand OnPlayCommand => new RelayCommand<object?>(_ => _onPlay());

    private double DurationSeconds =>
        _mediaPlayer.NaturalDuration.HasTimeSpan
            ? _mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds
            : 0;

    private void _onResume()
    {
        if (!_audioOpened)
        {
            var uri = new Uri(_audioFilePath, UriKind.Absolute);
            _mediaPlayer.Open(uri);
            _audioOpened = true;
        }

        IsPlaying = true;
        _mediaPlayer.Play();
        OnPlayStateChanged?.Invoke();
    }

    private void _onPause()
    {
        IsPlaying = false;
        _mediaPlayer.Pause();
        OnPlayStateChanged?.Invoke();
    }

    private void _onStop()
    {
        IsPlaying = false;
        _mediaPlayer.Stop();
    }

    private void _timerTick(object? sender, EventArgs e) =>
        OnPropertyChanged(nameof(ListenedPercentage));

    private void _onPlay()
    {
        if (!IsPlaying)
        {
            _audioService.Pause();
            _audioService.AudioPlayer = AudioPlayer;
            _audioService.Resume();
        }
        else
        {
            _onPause();
        }
    }

    private void _onAudioEnded(object? sender, EventArgs e)
    {
        IsPlaying = false;
        _audioService.Stop();

        if (_nextPlayer is not null)
        {
            _audioService.AudioPlayer = _nextPlayer.AudioPlayer;
        }
    }

    private async void _onAudioFailed(object? sender, ExceptionEventArgs e)
    {
        var content = !File.Exists(_audioFilePath)
            ? $"Soubor '{_audioFilePath}' nebyl nalezen"
            : $"Soubor '{_audioFilePath}' se nepodařilo přehrát: '{e.ErrorException.Message}'";

        var messageBox = new MessageBox
        {
            Title = "Chyba",
            Content = content,
            CloseButtonText = "Zavřít"
        };

        await messageBox.ShowDialogAsync();
    }
}