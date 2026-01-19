using System.IO;
using TestAdministration.Models.Data;

namespace TestAdministration.Models.Services;

public class AudioInstructionService
{
    private bool _isPlaying;

    public AudioPlayer? AudioPlayer
    {
        get;
        set
        {
            field = value;
            _isPlaying = false;
        }
    }

    /// <summary>
    /// Resumes or pauses the current audio player.
    /// </summary>
    public void Play()
    {
        if (AudioPlayer is null)
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
    /// Resumes the current audio player if not null.
    /// </summary>
    public void Resume()
    {
        _isPlaying = true;
        AudioPlayer?.OnResume.Invoke();
    }

    /// <summary>
    /// Pauses the current audio player if not null.
    /// </summary>
    public void Pause()
    {
        _isPlaying = false;
        AudioPlayer?.OnPause.Invoke();
    }

    /// <summary>
    /// Pauses the current audio player and clears audio player
    /// choice.
    /// </summary>
    public void Stop()
    {
        _isPlaying = false;
        AudioPlayer?.OnStop.Invoke();
        AudioPlayer = null;
    }

    /// <summary>
    /// Resolve the absolute file path of an audio file. The pattern is
    /// "{test}/{dominantHand}/{section}_{trial}_{index}(_{gender})(_{trialCount}).mp3"
    /// </summary>
    public static string GetFilePath(
        TestType testType,
        Hand dominantHand,
        int section,
        int trial,
        int index,
        bool? isMale = null,
        int? trialCount = null
    )
    {
        var test = testType.ToString().ToUpper();
        var hand = dominantHand == Hand.Right ? "PHK" : "LHK";

        List<object> fileNameParts = [section, trial, index];

        if (isMale is not null)
        {
            var gender = isMale.Value ? "m" : "z";
            fileNameParts.Add(gender);
        }

        if (trialCount is not null)
        {
            fileNameParts.Add(trialCount.Value);
        }

        return Path.Combine(
            AppContext.BaseDirectory,
            "Resources",
            "Audio",
            test,
            hand,
            string.Join("_", fileNameParts) + ".mp3"
        );
    }
}