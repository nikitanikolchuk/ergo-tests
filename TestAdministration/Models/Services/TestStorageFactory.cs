using System.ComponentModel;
using TestAdministration.Models.Data;
using TestAdministration.Models.Exporters;
using TestAdministration.Models.FileSystems;
using TestAdministration.Models.Importers;
using TestAdministration.Models.Storages;

namespace TestAdministration.Models.Services;

/// <summary>
/// A factory for runtime creation of <see cref="ITestStorage"/> objects.
/// </summary>
public class TestStorageFactory(
    LocalFileSystem localFileSystem,
    LocalCsvImporter localCsvImporter,
    LocalCsvExporter localCsvExporter
)
{
    public TestStorage Create(StorageType storageType) => storageType switch
    {
        StorageType.Local => _createLocal(),
        StorageType.SharePoint => throw new NotImplementedException(),
        _ => throw new InvalidEnumArgumentException(
            nameof(storageType),
            Convert.ToInt32(storageType),
            typeof(StorageType)
        )
    };

    private TestStorage _createLocal() => new(
        localFileSystem,
        localCsvImporter,
        localCsvExporter
    );
}