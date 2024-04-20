using System.IO;
using TestAdministration.Models.Services;

namespace TestAdministration.Models.Storages.FileSystems;

public class LocalFileSystem(
    ConfigurationService configurationService
) : IFileSystem
{
    public string TestDataPath => configurationService.LocalTestDataPath;

    public IEnumerable<string> GetSubdirectoryNames()
    {
        var directoryPath = configurationService.LocalTestDataPath;
        return Directory.GetDirectories(directoryPath, "*", SearchOption.TopDirectoryOnly)
            .Select(path => new DirectoryInfo(path).Name);
    }
}