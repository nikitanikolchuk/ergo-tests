using TestAdministration.Models.Data;

namespace TestAdministration.Models.Storages;

/// <summary>
/// A facade for importing/exporting test data. Abstracts
/// CSV file creation for different storage types. 
/// </summary>
public interface ITestStorage
{
    /// <summary>
    /// The path to the test data directory.
    /// </summary>
    public string DataPath { get; }

    /// <summary>
    /// Gets the patient from Patient.csv by converting
    /// directory info to the patient's directory name.
    /// </summary>
    public Patient? GetPatient(PatientDirectoryInfo patientDirectoryInfo);

    /// <summary>
    /// Parses patient directory names into a
    /// <see cref="PatientDirectoryInfo"/> list for identification.
    /// </summary>
    public List<PatientDirectoryInfo> GetAllPatientDirectoryInfos();

    /// <summary>
    /// Gets last test results from the [TEST_TYPE].csv from the
    /// patient's directory. 
    /// </summary>
    public Test? GetLastTestByPatient(TestType testType, Patient patient);

    /// <summary>
    /// Saves patient data into the patient's directory.
    /// </summary>
    public void AddPatient(Patient patient);

    /// <summary>
    /// Saves test data into the patient's directory. Additionally
    /// saves patient info if not present. 
    /// </summary>
    public void AddTest(Patient patient, Test test, List<string> videoFilePaths);
}