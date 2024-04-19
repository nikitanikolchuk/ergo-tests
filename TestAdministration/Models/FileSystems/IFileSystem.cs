namespace TestAdministration.Models.FileSystems;

/// <summary>
/// An interface for storage type-specific directory name
/// accessing.
/// </summary>
public interface IFileSystem
{
    public string TestDataPath { get; }
    
    /// <summary>
    /// Gets names of all top-level subdirectories from the
    /// storage type-specific test data directory defined in the
    /// application configuration. 
    /// </summary>
    public IEnumerable<string> GetSubdirectoryNames();
}