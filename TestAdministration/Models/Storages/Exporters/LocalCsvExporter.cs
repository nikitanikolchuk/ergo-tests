using System.ComponentModel;
using System.IO;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using TestAdministration.Models.Data;
using TestAdministration.Models.Storages.Converters;
using TestAdministration.Models.Storages.FileSystems;
using TestAdministration.Models.Storages.Records;

namespace TestAdministration.Models.Storages.Exporters;

/// <summary>
/// An implementation of <see cref="ICsvExporter"/> for local storage.
/// </summary>
public class LocalCsvExporter(
    LocalFileSystem fileSystem,
    PatientCsvConverter patientConverter,
    NhptCsvConverter nhptConverter,
    PptCsvConverter pptConverter,
    BbtCsvConverter bbtConverter
) : ICsvExporter
{
    private const string PatientFileName = "Pacient.csv";

    public void Export(Patient patient)
    {
        var patientDirectory = Path.Combine(fileSystem.TestDataPath, _directoryName(patient));
        Directory.CreateDirectory(patientDirectory);

        var patientPath = Path.Combine(patientDirectory, PatientFileName);
        if (!File.Exists(patientPath))
        {
            _exportPatient(patient, patientPath);
        }
    }

    public void Export(Patient patient, Test test)
    {
        var patientDirectory = Path.Combine(fileSystem.TestDataPath, _directoryName(patient));
        Directory.CreateDirectory(patientDirectory);

        var patientPath = Path.Combine(patientDirectory, PatientFileName);
        if (!File.Exists(patientPath))
        {
            _exportPatient(patient, patientPath);
        }

        var testType = test.Type.ToString().ToUpper();
        var testPath = Path.Combine(patientDirectory, $"{testType}.csv");
        _exportTest(test, testPath);
    }

    private static string _directoryName(Patient patient)
    {
        var surname = patient.Surname.ToUpper().Replace(" ", "-");
        var name = patient.Name.ToUpper().Replace(" ", "-");
        var id = patient.Id.Replace(" ", "-");
        return $"{surname}_{name}_{id}";
    }

    private void _exportPatient(Patient patient, string filePath)
    {
        using var stream = File.Open(filePath, FileMode.CreateNew);
        using var writer = new StreamWriter(stream, new UTF8Encoding(true));

        var config = CsvConfiguration.FromAttributes<PatientCsvRecord>();
        using var csvWriter = new CsvWriter(writer, config);
        var record = patientConverter.ToRecord(patient);

        csvWriter.WriteRecords([record]);
    }

    private void _exportTest(Test test, string filePath)
    {
        var fileExisted = File.Exists(filePath);
        using var stream = File.Open(filePath, FileMode.Append);
        using var writer = new StreamWriter(stream, new UTF8Encoding(true));

        var config = _getConfig(test.Type);
        config.HasHeaderRecord = !fileExisted;
        using var csvWriter = new CsvWriter(writer, config);

        _writeRecord(csvWriter, test);
    }

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

    private void _writeRecord(CsvWriter csvWriter, Test test)
    {
        switch (test.Type)
        {
            case TestType.Nhpt:
                var nhptRecord = nhptConverter.ToRecord(test);
                csvWriter.WriteRecords([nhptRecord]);
                break;
            case TestType.Ppt:
                var pptRecord = pptConverter.ToRecord(test);
                csvWriter.WriteRecords([pptRecord]);
                break;
            case TestType.Bbt:
                var bbtRecord = bbtConverter.ToRecord(test);
                csvWriter.WriteRecords([bbtRecord]);
                break;
            default:
                throw new InvalidEnumArgumentException(
                    nameof(test.Type),
                    Convert.ToInt32(test.Type),
                    typeof(TestType)
                );
        }
    }
}