using System.Collections.Immutable;
using System.IO;
using System.Reflection;
using System.Text.Json;
using TestAdministration.Models.Data;

namespace TestAdministration.Models.Services;

/// <summary>
/// A class for storing application configuration data in a
/// data.json file located in the same directory as the .exe file.
/// </summary>
public class LocalStorageService
{
    private const string FileName = "data.json";

    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        WriteIndented = true
    };

    private readonly string _filePath = _getFilePath();

    public string SharePointTestDataPath
    {
        get => _readData().SharePointTestDataPath;
        set
        {
            var data = _readData();
            data.SharePointTestDataPath = value;
            _writeData(data);
        }
    }

    public string LocalTestDataPath
    {
        get => _readData().LocalTestDataPath;
        set
        {
            var data = _readData();
            data.LocalTestDataPath = value;
            _writeData(data);
        }
    }

    public ImmutableList<string> LocalTesters
    {
        get => _readData().LocalTesters;
        set
        {
            var data = _readData();
            data.LocalTesters = value;
            _writeData(data);
        }
    }

    private static string _getFilePath()
    {
        var exePath = Assembly.GetExecutingAssembly().Location;
        var exeDirectoryPath = Path.GetDirectoryName(exePath)
                               ?? throw new ArgumentException("Can't get exe directory");
        return Path.Combine(exeDirectoryPath, FileName);
    }

    private LocalStorageData _readData()
    {
        if (!File.Exists(_filePath))
        {
            _writeData(new LocalStorageData());
        }

        using var fileStream = File.OpenRead(_filePath);
        return JsonSerializer.Deserialize<LocalStorageData>(fileStream)
               ?? new LocalStorageData();
    }

    private void _writeData(LocalStorageData data)
    {
        using var fileStream = File.Create(_filePath);
        JsonSerializer.Serialize(fileStream, data, JsonSerializerOptions);
        fileStream.Flush();
    }
}