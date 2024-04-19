using System.IO;
using TestAdministration.Models.Services;

namespace TestAdministration.Models.FileSystems;

public class LocalFileSystem(
    ConfigurationService configurationService
) : IFileSystem
{
    public string TestDataPath => configurationService.LocalTestDataPath;

    public IEnumerable<string> GetSubdirectoryNames()
    {
        var directoryPath = configurationService.LocalTestDataPath;
        return Directory.GetDirectories(directoryPath, "*", SearchOption.TopDirectoryOnly);
    }
}