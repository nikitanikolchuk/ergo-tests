using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using TestAdministration.Models.Services;
using Wpf.Ui.Input;

namespace TestAdministration.ViewModels.Testing.Instructions;

/// <summary>
/// A view model for an audio player for instructions.
/// </summary>
public class InstructionPlayerViewModel : ViewModelBase
{
    private readonly AudioInstructionService _audioService;
    private readonly MediaPlayer _mediaPlayer;
    private readonly InstructionPlayerViewModel? _nextPlayer;

    private bool _isPlaying;

    public InstructionPlayerViewModel(
        AudioInstructionService audioService,
        MediaPlayer mediaPlayer,
        InstructionPlayerViewModel? nextPlayer
    )
    {
        _audioService = audioService;
        _mediaPlayer = mediaPlayer;
        _nextPlayer = nextPlayer;

        _mediaPlayer.MediaEnded += _onAudioEnded;

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
        get => _isPlaying;
        private set
        {
            _isPlaying = value;
            OnPropertyChanged();
        }
    }

    public ICommand OnPlayCommand => new RelayCommand<object?>(_ => _onPlay());

    private double DurationSeconds =>
        _mediaPlayer.NaturalDuration.HasTimeSpan
            ? _mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds
            : 0;

    public void OnResume()
    {
        IsPlaying = true;
        _mediaPlayer.Play();
        OnPlayStateChanged?.Invoke();
    }

    public void OnPause()
    {
        IsPlaying = false;
        _mediaPlayer.Pause();
        OnPlayStateChanged?.Invoke();
    }

    public void OnStop()
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
            _audioService.SetPlayerActions(OnResume, OnPause, OnStop);
            _audioService.Resume();
        }
        else
        {
            OnPause();
        }
    }

    private void _onAudioEnded(object? sender, EventArgs e)
    {
        IsPlaying = false;
        _audioService.Stop();

        if (_nextPlayer is not null)
        {
            _audioService.SetPlayerActions(_nextPlayer.OnResume, _nextPlayer.OnPause, _nextPlayer.OnStop);
        }
    }
}