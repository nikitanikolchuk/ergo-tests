using TestAdministration.Models.Data;

namespace TestAdministration.Models.Storages;

/// <summary>
/// An interface for importing/exporting test data. Abstracts
/// CSV file creation for different storage types. 
/// </summary>
public interface ITestStorage
{
    /// <summary>
    /// The path to the test data directory.
    /// </summary>
    public string DataPath { get; }

    /// <summary>
    /// Gets the patient from Patient.csv by ID specified in the
    /// corresponding directory's name.
    /// </summary>
    public Patient? GetPatientById(string id);

    /// <summary>
    /// Parses patient directory names into a
    /// <see cref="PatientDirectoryInfo"/> list for identification.
    /// </summary>
    public IEnumerable<PatientDirectoryInfo> GetAllPatientsShort();

    /// <summary>
    /// Gets last test results from the [TEST_TYPE].csv by ID
    /// specified in the corresponding directory's name. 
    /// </summary>
    public Test? GetLastTestByPatientId(TestType testType, string patientId);

    /// <summary>
    /// Saves test data into the patient's directory. Additionally
    /// saves patient info if not present. 
    /// </summary>
    public void AddTest(Patient patient, Test test);
}