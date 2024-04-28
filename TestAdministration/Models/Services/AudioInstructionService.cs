using System.IO;
using System.Windows.Media;
using TestAdministration.Models.Data;

namespace TestAdministration.Models.Services;

public class AudioInstructionService
{
    private const string ResourcesPath = "pack://siteoforigin:,,,/Resources/Audio/";

    private MediaPlayer? _currentAudioPlayer;

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
    /// Pauses the current audio player (if not null) before
    /// using the next one.
    /// </summary>
    public void Play(MediaPlayer mediaPlayer)
    {
        _currentAudioPlayer?.Pause();
        _currentAudioPlayer = mediaPlayer;
        _currentAudioPlayer.Play();
    }

    /// <summary>
    /// Pauses the current audio player if not null.
    /// </summary>
    public void Pause()
    {
        _currentAudioPlayer?.Pause();
    }
}