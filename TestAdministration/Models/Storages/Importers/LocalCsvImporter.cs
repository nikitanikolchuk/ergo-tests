using System.IO;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using TestAdministration.Models.Data;
using TestAdministration.Models.Storages.FileSystems;
using TestAdministration.Models.Storages.Mappers;

namespace TestAdministration.Models.Storages.Importers;

/// <summary>
/// An implementation of <see cref="ICsvImporter"/> for local storage.
/// </summary>
public class LocalCsvImporter(
    LocalFileSystem fileSystem,
    PatientCsvConverter patientCsvConverter
) : ICsvImporter
{
    // TODO: move to config file
    private const string PatientFileName = "Pacient.csv";

    public Patient? GetPatientById(string id)
    {
        var patientDirectoryName = fileSystem.GetSubdirectoryNames()
            .FirstOrDefault(dir =>
            {
                var parts = dir.Split('_');
                return parts.Length == 3 && parts.Last() == id;
            });

        if (patientDirectoryName is null)
        {
            return null;
        }

        var patientPath = Path.Combine(fileSystem.TestDataPath, patientDirectoryName, PatientFileName);
        if (!File.Exists(patientPath))
        {
            return null;
        }

        using var stream = File.Open(patientPath, FileMode.Open);
        using var reader = new StreamReader(stream, new UTF8Encoding(true));

        var config = CsvConfiguration.FromAttributes<PatientCsvRecord>();
        using var csvReader = new CsvReader(reader, config);

        Patient patient;
        try
        {
            var record = csvReader.GetRecords<PatientCsvRecord>().FirstOrDefault();
            if (record is null)
            {
                return null;
            }

            patient = patientCsvConverter.FromRecord(record);
        }
        catch (Exception e) when (e is HeaderValidationException or CsvConverterException)
        {
            return null;
        }

        return patient;
    }

    public Test? GetLastTestByPatientId(TestType testType, string patientId)
    {
        throw new NotImplementedException();
    }
}