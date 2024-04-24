using System.ComponentModel;
using System.IO;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using TestAdministration.Models.Data;
using TestAdministration.Models.Storages.Converters;
using TestAdministration.Models.Storages.FileSystems;
using TestAdministration.Models.Storages.Records;

namespace TestAdministration.Models.Storages.Importers;

/// <summary>
/// An implementation of <see cref="ICsvImporter"/> for local storage.
/// </summary>
public class LocalCsvImporter(
    LocalFileSystem fileSystem,
    PatientCsvConverter patientConverter,
    NhptCsvConverter nhptConverter,
    PptCsvConverter pptConverter,
    BbtCsvConverter bbtConverter
) : ICsvImporter
{
    // TODO: move to config file
    private const string PatientFileName = "Pacient.csv";

    public Patient? ImportPatientById(string id)
    {
        var patientDirectoryName = _patientDirectoryName(id);
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

        try
        {
            var record = csvReader.GetRecords<PatientCsvRecord>().FirstOrDefault();
            return record is null ? null : patientConverter.FromRecord(record);
        }
        catch (Exception e) when (e is CsvHelperException or CsvConverterException)
        {
            return null;
        }
    }

    public Test? ImportTestByPatientId(TestType testType, string patientId)
    {
        var patientDirectoryName = _patientDirectoryName(patientId);
        if (patientDirectoryName is null)
        {
            return null;
        }

        var fileName = $"{testType.ToString().ToUpper()}.csv";
        var filePath = Path.Combine(fileSystem.TestDataPath, patientDirectoryName, fileName);
        if (!File.Exists(filePath))
        {
            return null;
        }

        using var stream = File.Open(filePath, FileMode.Open);
        using var reader = new StreamReader(stream, new UTF8Encoding(true));

        var config = _getConfig(testType);
        using var csvReader = new CsvReader(reader, config);

        try
        {
            return _readTest(csvReader, testType);
        }
        catch (CsvHelperException)
        {
            return null;
        }
    }

    private string? _patientDirectoryName(string patientId) =>
        fileSystem.GetSubdirectoryNames()
            .FirstOrDefault(dir =>
            {
                var parts = dir.Split('_');
                return parts.Length == 3 && parts.Last() == patientId;
            });


    private static CsvConfiguration _getConfig(TestType testType) => testType switch
    {
        TestType.Nhpt => CsvConfiguration.FromAttributes<NhptCsvRecord>(),
        TestType.Ppt => CsvConfiguration.FromAttributes<PptCsvRecord>(),
        TestType.Bbt => CsvConfiguration.FromAttributes<BbtCsvRecord>(),
        _ => throw new InvalidEnumArgumentException(
            nameof(testType),
            Convert.ToInt32(testType),
            typeof(TestType)
        )
    };

    private Test? _readTest(CsvReader csvReader, TestType testType)
    {
        switch (testType)
        {
            case TestType.Nhpt:
                var nhptRecord = csvReader.GetRecords<NhptCsvRecord>().LastOrDefault();
                return nhptRecord is null ? null : nhptConverter.FromRecord(nhptRecord);
            case TestType.Ppt:
                var pptRecord = csvReader.GetRecords<PptCsvRecord>().LastOrDefault();
                return pptRecord is null ? null : pptConverter.FromRecord(pptRecord);
            case TestType.Bbt:
                var bbtRecord = csvReader.GetRecords<BbtCsvRecord>().LastOrDefault();
                return bbtRecord is null ? null : bbtConverter.FromRecord(bbtRecord);
            default:
                throw new InvalidEnumArgumentException(
                    nameof(testType),
                    Convert.ToInt32(testType),
                    typeof(TestType)
                );
        }
    }
}