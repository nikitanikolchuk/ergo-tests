using TestAdministration.Models.Data;

namespace TestAdministration.Models.Storages.Exporters;

/// <summary>
/// An interface for exporting test data.
/// </summary>
public interface ICsvExporter
{
    /// <summary>
    /// Exports the patient's personal data into the Pacient.csv
    /// file inside the patient's directory if it does not
    /// already exist.
    /// </summary>
    void Export(Patient patient);

    /// <summary>
    /// Exports test data to a CSV file inside the patient's
    /// directory. Creates corresponding Patient.csv and
    /// [TEST_TYPE].csv files if not present.
    /// </summary>
    void Export(Patient patient, Test test);
}