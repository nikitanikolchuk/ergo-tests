using System.Collections.Immutable;
using System.IO;
using System.Text.Json;
using TestAdministration.Models.Data;
using Wpf.Ui.Appearance;

namespace TestAdministration.Models.Services;

/// <summary>
/// A class for storing application configuration data in a
/// data.json file located in the same directory as the .exe file.
/// Loads data.json during creation and updates it when any value
/// changes.
/// </summary>
public class ConfigurationService
{
    private const string FileName = "data.json";

    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        WriteIndented = true
    };

    private readonly string _filePath;

    private string _localTestDataPath;
    private ImmutableList<string> _localUsers;
    private string _currentUser;
    private string _applicationTheme;
    private string _fontSize;
    private string _cameraId;
    private string _microphoneId;

    public ConfigurationService()
    {
        _filePath = _getFilePath();

        var data = _readData();
        _localTestDataPath = data.LocalTestDataPath;
        _localUsers = data.LocalUsers;
        _currentUser = data.CurrentUser;
        _applicationTheme = data.ApplicationTheme;
        _fontSize = data.FontSize;
        _cameraId = data.CameraId;
        _microphoneId = data.MicrophoneId;
    }

    public string LocalTestDataPath
    {
        get => _localTestDataPath;
        set
        {
            _localTestDataPath = value;
            _updateData();
        }
    }

    public ImmutableList<string> LocalUsers
    {
        get => _localUsers;
        set
        {
            _localUsers = value;
            _updateData();
        }
    }

    public string CurrentUser
    {
        get => _currentUser;
        set
        {
            _currentUser = value;
            _updateData();
        }
    }

    public ApplicationTheme ApplicationTheme
    {
        get
        {
            var themeString = _applicationTheme;
            return themeString == ApplicationTheme.Dark.ToString()
                ? ApplicationTheme.Dark
                : ApplicationTheme.Light;
        }
        set
        {
            _applicationTheme = value.ToString();
            _updateData();
        }
    }

    public int FontSize
    {
        get
        {
            var sizeString = _fontSize;
            return int.TryParse(sizeString, out var size)
                ? size
                : ConfigurationData.DefaultFontSize;
        }
        set
        {
            _fontSize = value.ToString();
            _updateData();
        }
    }

    public int CameraId
    {
        get => int.TryParse(_cameraId, out var id) ? id : 0;
        set
        {
            _cameraId = value.ToString();
            _updateData();
        }
    }

    public int MicrophoneId
    {
        get => int.TryParse(_microphoneId, out var id) ? id : 0;
        set
        {
            _microphoneId = value.ToString();
            _updateData();
        }
    }

    private static string _getFilePath() => Path.Combine(AppContext.BaseDirectory, FileName);

    private ConfigurationData _readData()
    {
        if (!File.Exists(_filePath))
        {
            _writeData(new ConfigurationData());
        }

        using var fileStream = File.OpenRead(_filePath);
        return JsonSerializer.Deserialize<ConfigurationData>(fileStream)
               ?? new ConfigurationData();
    }

    private void _writeData(ConfigurationData data)
    {
        using var fileStream = File.Create(_filePath);
        JsonSerializer.Serialize(fileStream, data, JsonSerializerOptions);
        fileStream.Flush();
    }

    private void _updateData()
    {
        var data = new ConfigurationData
        {
            LocalTestDataPath = _localTestDataPath,
            LocalUsers = _localUsers,
            ApplicationTheme = _applicationTheme,
            CurrentUser = _currentUser,
            FontSize = _fontSize,
            CameraId = _cameraId,
            MicrophoneId = _microphoneId
        };

        _writeData(data);
    }
}