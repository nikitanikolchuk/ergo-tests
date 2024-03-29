using System.Globalization;
using System.IO;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using TestAdministration.Models.Data;

namespace TestAdministration.Models.Exporters;

/// <summary>
/// An implementation of <c>ICsvExporter</c> for local storage.
/// </summary>
/// <param name="patientMapper">CSV mapper for <c>Test</c> objects.</param>
/// <param name="testMapper">Test specific CSV mapper for <c>Test</c> objects.</param>
/// <param name="path">The path for application-wide test results directory.</param>
public class LocalCsvExporter(
    ClassMap<Patient> patientMapper,
    ClassMap<Test> testMapper,
    string path
) : ICsvExporter
{
    private const string Delimiter = ";";

    public void Export(Patient patient, Test test)
    {
        var patientDirectory = Path.Combine(path, _directoryName(patient));
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
        csvWriter.Context.RegisterClassMap(testMapper);

        csvWriter.WriteRecords([test]);
    }
}