using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using TestAdministration.Models.Data;
using TestAdministration.Models.Services;
using TestAdministration.Models.Storages.Mappers;

namespace TestAdministration.Models.Storages.Exporters;

/// <summary>
/// An implementation of <see cref="ICsvExporter"/> for local storage.
/// </summary>
// TODO: add PPT and BBT mappers
public class LocalCsvExporter(
    ConfigurationService configurationService,
    ClassMap<Patient> patientMapper,
    NhptCsvMapper nhptMapper
) : ICsvExporter
{
    private const string Delimiter = ";";

    public void Export(Patient patient, Test test)
    {
        var patientDirectory = Path.Combine(configurationService.LocalTestDataPath, _directoryName(patient));
        Directory.CreateDirectory(patientDirectory);

        var patientPath = Path.Combine(patientDirectory, "Pacient.csv");
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
        return $"{surname}_{name}";
    }

    private void _exportPatient(Patient patient, string filePath)
    {
        using var stream = File.Open(filePath, FileMode.CreateNew);
        using var writer = new StreamWriter(stream, new UTF8Encoding(true));

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = Delimiter,
            HasHeaderRecord = true
        };
        using var csvWriter = new CsvWriter(writer, config);
        csvWriter.Context.RegisterClassMap(patientMapper);

        csvWriter.WriteRecords([patient]);
    }

    private void _exportTest(Test test, string filePath)
    {
        var fileExisted = File.Exists(filePath);
        using var stream = File.Open(filePath, FileMode.Append);
        using var writer = new StreamWriter(stream, new UTF8Encoding(true));

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = Delimiter,
            HasHeaderRecord = !fileExisted
        };
        using var csvWriter = new CsvWriter(writer, config);
        var testMapper = _getMapper(test);
        csvWriter.Context.RegisterClassMap(testMapper);

        csvWriter.WriteRecords([test]);
    }

    private ClassMap<Test> _getMapper(Test test) => test.Type switch
    {
        TestType.Nhpt => nhptMapper,
        TestType.Ppt => throw new NotImplementedException(),
        TestType.Bbt => throw new NotImplementedException(),
        _ => throw new InvalidEnumArgumentException(
            nameof(test.Type),
            Convert.ToInt32(test.Type),
            typeof(TestType)
        )
    };
}