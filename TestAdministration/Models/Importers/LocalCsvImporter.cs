using TestAdministration.Models.Data;

namespace TestAdministration.Models.Importers;

public class LocalCsvImporter : ICsvImporter
{
    public Patient? GetPatientById(string id)
    {
        throw new NotImplementedException();
    }

    public Test? GetLastTestByPatientId(TestType testType, string patientId)
    {
        throw new NotImplementedException();
    }
}