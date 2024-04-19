using TestAdministration.Models.Data;

namespace TestAdministration.Models.Importers;

/// <summary>
/// An interface for importing test data.
/// </summary>
public interface ICsvImporter
{
    /// <summary>
    /// Gets the patient from Patient.csv by ID specified in the
    /// corresponding directory's name.
    /// </summary>
    public Patient? GetPatientById(string id);

    /// <summary>
    /// Gets last test results from the [TEST_TYPE].csv by ID
    /// specified in the corresponding directory's name. 
    /// </summary>
    public Test? GetLastTestByPatientId(TestType testType, string patientId);
}