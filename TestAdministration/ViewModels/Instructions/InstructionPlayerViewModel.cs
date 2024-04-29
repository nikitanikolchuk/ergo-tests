using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using TestAdministration.Models.Services;
using Wpf.Ui.Input;

namespace TestAdministration.ViewModels.Instructions;

/// <summary>
/// A view model for an audio player for instructions.
/// </summary>
public class InstructionPlayerViewModel : ViewModelBase
{
    private readonly AudioInstructionService _audioService;
    private readonly MediaPlayer _mediaPlayer;
    private bool _isPlaying;

    public InstructionPlayerViewModel(AudioInstructionService audioService, MediaPlayer mediaPlayer)
    {
        _audioService = audioService;
        _mediaPlayer = mediaPlayer;

        _mediaPlayer.MediaEnded += _onAudioEnded;

        var timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(1)
        };
        timer.Tick += _timerTick;
        timer.Start();
    }

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

    public ICommand OnPlayCommand => new RelayCommand<object?>(_ =>
    {
        if (!IsPlaying)
        {
            _audioService.Pause();
            _audioService.SetPlayerActions(_onPlay, _onPause);
            _audioService.Play();
        }
        else
        {
            _onPause();
        }
    });

    private double DurationSeconds =>
        _mediaPlayer.NaturalDuration.HasTimeSpan
            ? _mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds
            : 0;

    private void _timerTick(object? sender, EventArgs e) =>
        OnPropertyChanged(nameof(ListenedPercentage));

    private void _onPlay()
    {
        IsPlaying = true;
        _mediaPlayer.Play();
    }

    private void _onPause()
    {
        IsPlaying = false;
        _mediaPlayer.Pause();
    }

    private void _onAudioEnded(object? sender, EventArgs e)
    {
        IsPlaying = false;
        _mediaPlayer.Stop();
    }
}