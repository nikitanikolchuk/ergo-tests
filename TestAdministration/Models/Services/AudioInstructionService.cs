using System.IO;
using System.Windows.Media;
using TestAdministration.Models.Data;

namespace TestAdministration.Models.Services;

public class AudioInstructionService
{
    private const string ResourcesPath = "pack://siteoforigin:,,,/Resources/Audio/";

    private bool _isPlaying;
    private Action? _onResume;
    private Action? _onPause;
    private Action? _onStop;

    public MediaPlayer Get(
        TestType testType,
        Hand dominantHand,
        int section,
        int trial,
        int index,
        bool? isMale = null
    )
    {
        var test = testType.ToString().ToUpper();
        var hand = dominantHand == Hand.Right ? "PHK" : "LHK";

        string filePath;
        if (isMale != null)
        {
            var gender = isMale.Value ? "m" : "z";
            filePath = $"{test}/{hand}/{section}_{trial}_{index}_{gender}.mp3";
        }
        else
        {
            filePath = $"{test}/{hand}/{section}_{trial}_{index}.mp3";
        }

        var uri = new Uri(Path.Combine(ResourcesPath, filePath));

        var mediaPlayer = new MediaPlayer();
        mediaPlayer.Open(uri);

        return mediaPlayer;
    }

    /// <summary>
    /// Resumes or pauses the current audio player.
    /// </summary>
    public void Play()
    {
        if (_onResume is null || _onPause is null)
        {
            return;
        }

        if (!_isPlaying)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    /// <summary>
    /// Sets the current audio player actions that account for
    /// custom play/pause logic.
    /// </summary>
    public void SetPlayerActions(Action? onResume, Action? onPause, Action? onStop)
    {
        _onResume = onResume;
        _onPause = onPause;
        _onStop = onStop;
    }

    /// <summary>
    /// Resumes the current audio player if not null.
    /// </summary>
    public void Resume()
    {
        _isPlaying = true;
        _onResume?.Invoke();
    }

    /// <summary>
    /// Pauses the current audio player if not null.
    /// </summary>
    public void Pause()
    {
        _isPlaying = false;
        _onPause?.Invoke();
    }

    /// <summary>
    /// Pauses the current audio player and clears audio player
    /// choice.
    /// </summary>
    public void Stop()
    {
        _isPlaying = false;
        _onStop?.Invoke();
        SetPlayerActions(null, null, null);
    }
}