using System.Diagnostics;

namespace TestAdministration.ViewModels;

/// <summary>
/// A view model for binding initial content of the main screen.
/// </summary>
public class InitContentViewModel : ViewModelBase
{
    public string AppVersion => _getFileVersion();

    private static string _getFileVersion()
    {
        var processPath = Environment.ProcessPath;
        if (processPath is null)
        {
            return string.Empty;
        }
        
        var fileVersion = FileVersionInfo.GetVersionInfo(processPath).FileVersion;
        if (fileVersion is null)
        {
            return string.Empty;
        }
        
        var token = fileVersion.Split(' ', StringSplitOptions.RemoveEmptyEntries).First();

        return Version.TryParse(token, out var version)
            ? $"{version.Major}.{version.Minor}.{version.Build}"
            : fileVersion;
    }
}