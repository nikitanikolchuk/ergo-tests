using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Wpf.Ui.Input;

namespace TestAdministration.ViewModels.Instructions;

/// <summary>
/// A view model for an audio player for instructions.
/// </summary>
public class InstructionPlayerViewModel : ViewModelBase
{
    private readonly MediaPlayer _mediaPlayer;

    public InstructionPlayerViewModel(MediaPlayer mediaPlayer)
    {
        _mediaPlayer = mediaPlayer;

        var timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(1)
        };
        timer.Tick += _timerTick;
        timer.Start();
    }

    public string Time { get; private set; } = "00:00 / 00:00";
    public ICommand PlayCommand => new RelayCommand<object?>(_ => _mediaPlayer.Play());
    public ICommand PauseCommand => new RelayCommand<object?>(_ => _mediaPlayer.Pause());
    public ICommand StopCommand => new RelayCommand<object?>(_ => _mediaPlayer.Stop());

    private void _timerTick(object? sender, EventArgs e)
    {
        if (!_mediaPlayer.NaturalDuration.HasTimeSpan)
        {
            return;
        }

        Time = $@"{_mediaPlayer.Position:mm\:ss} / {_mediaPlayer.NaturalDuration.TimeSpan:mm\:ss}";
        OnPropertyChanged(nameof(Time));
    }
}