using TestAdministration.Models.Data;

namespace TestAdministration.Models.Storages.Exporters;

/// <summary>
/// An interface for exporting test data.
/// </summary>
public interface ICsvExporter
{
    /// <summary>
    /// Exports test data to a CSV file inside the patient's
    /// directory. Creates corresponding Patient.csv and
    /// [TEST_TYPE].csv files if not present.
    /// </summary>
    /// <param name="patient">The tested patient.</param>
    /// <param name="test">The test data.</param>
    void Export(Patient patient, Test test);
}