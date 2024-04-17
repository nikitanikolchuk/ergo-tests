using System.Collections.Immutable;

namespace TestAdministration.Models.Data;

/// <summary>
/// A class for JSON serialization.
/// </summary>
public class LocalStorageData
{
    public string SharePointTestDataPath { get; set; } = "";
    public string LocalTestDataPath { get; set; } = "";
    public ImmutableList<string> LocalUsers { get; set; } = [];
}