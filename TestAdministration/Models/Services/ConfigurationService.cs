using System.Collections.Immutable;
using System.IO;
using System.Reflection;
using System.Text.Json;
using TestAdministration.Models.Data;
using Wpf.Ui.Appearance;

namespace TestAdministration.Models.Services;

/// <summary>
/// A class for storing application configuration data in a
/// data.json file located in the same directory as the .exe file.
/// </summary>
public class ConfigurationService
{
    private const string FileName = "data.json";
    private const int DefaultFontSize = 14;

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

    public ImmutableList<string> LocalUsers
    {
        get => _readData().LocalUsers;
        set
        {
            var data = _readData();
            data.LocalUsers = value;
            _writeData(data);
        }
    }

    public ApplicationTheme ApplicationTheme
    {
        get
        {
            var themeString = _readData().ApplicationTheme;
            return themeString == ApplicationTheme.Dark.ToString()
                ? ApplicationTheme.Dark
                : ApplicationTheme.Light;
        }
        set
        {
            var data = _readData();
            data.ApplicationTheme = value.ToString();
            _writeData(data);
        }
    }

    public int FontSize
    {
        get
        {
            var sizeString = _readData().FontSize;
            return int.TryParse(sizeString, out var size)
                ? size
                : DefaultFontSize;
        }
        set
        {
            var data = _readData();
            data.FontSize = value.ToString();
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

    private static ConfigurationData _createDefaultData() => new()
    {
        SharePointTestDataPath = string.Empty,
        LocalTestDataPath = string.Empty,
        LocalUsers = [],
        ApplicationTheme = ApplicationTheme.Light.ToString(),
        FontSize = DefaultFontSize.ToString(),
    };

    private ConfigurationData _readData()
    {
        if (!File.Exists(_filePath))
        {
            _writeData(_createDefaultData());
        }

        using var fileStream = File.OpenRead(_filePath);
        return JsonSerializer.Deserialize<ConfigurationData>(fileStream)
               ?? _createDefaultData();
    }

    private void _writeData(ConfigurationData data)
    {
        using var fileStream = File.Create(_filePath);
        JsonSerializer.Serialize(fileStream, data, JsonSerializerOptions);
        fileStream.Flush();
    }
}