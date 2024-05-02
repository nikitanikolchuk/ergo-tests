using System.Globalization;
using System.IO;
using System.Text;
using TestAdministration.Models.Data;
using TestAdministration.Models.Storages.Converters;
using TestAdministration.Models.Storages.FileSystems;

namespace TestAdministration.Models.Storages.Exporters;

/// <summary>
/// A class for exporting text files with content intended for
/// copying to medical documentation.
/// </summary>
public class DocumentationExporter(
    IFileSystem fileSystem,
    DocumentationConverter converter
)
{
    private const string DirectoryName = "Dokumentace";

    public void Export(Patient patient, Test test)
    {
        var directoryPath = Path.Combine(fileSystem.TestDataPath, _patientDirectoryName(patient), DirectoryName);
        Directory.CreateDirectory(directoryPath);

        var fileName = _getFileName(patient, test);
        var filePath = Path.Combine(directoryPath, fileName);

        var text = converter.Convert(test);
        File.WriteAllText(filePath, text);
    }

    private static string _patientDirectoryName(Patient patient)
    {
        var surname = patient.Surname.ToUpper().Replace(" ", "-");
        var name = patient.Name.ToUpper().Replace(" ", "-");
        var id = patient.Id.Replace(" ", "-");
        return _removeDiacritics($"{surname}_{name}_{id}");
    }

    private static string _getFileName(Patient patient, Test test)
    {
        var surname = patient.Surname.ToUpper().Replace(" ", "-");
        var name = patient.Name.ToUpper().Replace(" ", "-");
        var testType = test.Type.ToString().ToUpper();
        var date = test.Date.ToString("yyyy_MM_dd");

        return _removeDiacritics($"{surname}_{name}_{testType}_{date}.txt");
    }

    private static string _removeDiacritics(string str)
    {
        var chars = str
            .Normalize(NormalizationForm.FormD)
            .Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            .ToArray();

        return new string(chars);
    }
}