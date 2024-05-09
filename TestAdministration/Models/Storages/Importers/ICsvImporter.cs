using TestAdministration.Models.Data;

namespace TestAdministration.Models.Storages.Importers;

/// <summary>
/// An interface for importing test data.
/// </summary>
public interface ICsvImporter
{
    /// <summary>
    /// Gets the patient from Patient.csv by converting
    /// directory info to the patient's directory name.
    /// </summary>
    public Patient? ImportPatient(PatientDirectoryInfo patientDirectoryInfo);

    /// <summary>
    /// Gets last test results from the [TEST_TYPE].csv from the
    /// patient's directory. 
    /// </summary>
    public Test? ImportLastTestByPatient(TestType testType, Patient patient);
}