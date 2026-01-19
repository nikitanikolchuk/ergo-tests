namespace TestAdministration.Models.Data;

/// <summary>
/// Class for passing functions related to a single audio player.
/// </summary>
public record AudioPlayer(
    Action OnResume,
    Action OnPause,
    Action OnStop
);