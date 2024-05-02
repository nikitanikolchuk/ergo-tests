using System.Collections.Immutable;
using Wpf.Ui.Appearance;

namespace TestAdministration.Models.Data;

/// <summary>
/// A class for JSON serialization.
/// </summary>
public class ConfigurationData
{
    public const int DefaultFontSize = 14;

    private const ApplicationTheme DefaultTheme = Wpf.Ui.Appearance.ApplicationTheme.Light;

    public string LocalTestDataPath { get; set; } = string.Empty;
    public ImmutableList<string> LocalUsers { get; set; } = [];
    public string CurrentUser { get; set; } = string.Empty;
    public string ApplicationTheme { get; set; } = DefaultTheme.ToString();
    public string FontSize { get; set; } = DefaultFontSize.ToString();
}