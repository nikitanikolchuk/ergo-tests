using System.Collections.Immutable;

namespace TestAdministration.Models.Data;

/// <summary>
/// A class for JSON serialization.
/// </summary>
public class ConfigurationData
{
    public string SharePointTestDataPath { get; set; } = string.Empty;
    public string LocalTestDataPath { get; set; } = string.Empty;
    public ImmutableList<string> LocalUsers { get; set; } = [];
    public string ApplicationTheme { get; set; } = Wpf.Ui.Appearance.ApplicationTheme.Light.ToString();
    public string FontSize { get; set; } = string.Empty;
}