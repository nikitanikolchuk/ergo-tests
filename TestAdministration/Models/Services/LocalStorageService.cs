using System.Collections.Immutable;
using System.IO;
using System.Text.Json;
using TestAdministration.Models.Data;

namespace TestAdministration.Models.Services;

/// <summary>
/// A class for storing application configuration data in a
/// JSON file.
/// </summary>
public class LocalStorageService(string filePath)
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        WriteIndented = true
    };

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

    private LocalStorageData _readData()
    {
        if (!File.Exists(filePath))
        {
            _writeData(new LocalStorageData());
        }

        using var fileStream = File.OpenRead(filePath);
        return JsonSerializer.Deserialize<LocalStorageData>(fileStream)
               ?? new LocalStorageData();
    }

    private void _writeData(LocalStorageData data)
    {
        using var fileStream = File.Create(filePath);
        JsonSerializer.Serialize(fileStream, data, JsonSerializerOptions);
        fileStream.Flush();
    }
}