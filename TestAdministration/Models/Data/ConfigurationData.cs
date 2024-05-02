using System.Collections.Immutable;

namespace TestAdministration.Models.Data;

/// <summary>
/// A class for JSON serialization.
/// </summary>
public class ConfigurationData
{
    public required string LocalTestDataPath { get; set; }
    public required ImmutableList<string> LocalUsers { get; set; }
    public required string ApplicationTheme { get; set; }
    public required string FontSize { get; set; }
}