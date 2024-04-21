using TestAdministration.Models.Data;
using TestAdministration.Models.Storages.Exporters;
using TestAdministration.Models.Storages.FileSystems;
using TestAdministration.Models.Storages.Importers;

namespace TestAdministration.Models.Storages;

public class TestStorage(
    IFileSystem fileSystem,
    ICsvImporter csvImporter,
    ICsvExporter csvExporter
) : ITestStorage
{
    private const string DirectoryNameSeparator = "_";

    public string DataPath => fileSystem.TestDataPath;

    public IEnumerable<PatientDirectoryInfo> GetAllPatientDirectoryInfos() =>
        fileSystem.GetSubdirectoryNames()
            .Select(_patientDirectoryInfoFromName)
            .OfType<PatientDirectoryInfo>();

    public Patient? GetPatientById(string id) => csvImporter.GetPatientById(id);

    public Test? GetLastTestByPatientId(TestType testType, string patientId) =>
        csvImporter.GetLastTestByPatientId(testType, patientId);

    public void AddPatient(Patient patient) => csvExporter.Export(patient);

    public void AddTest(Patient patient, Test test) => csvExporter.Export(patient, test);

    private static PatientDirectoryInfo? _patientDirectoryInfoFromName(string directoryName)
    {
        var parts = directoryName.Split(DirectoryNameSeparator);
        if (parts.Length != 3)
        {
            return null;
        }

        var surname = parts[0].Replace('-', ' ');
        var name = parts[1].Replace('-', ' ');
        var id = parts[2];

        return new PatientDirectoryInfo(id, name, surname);
    }
}