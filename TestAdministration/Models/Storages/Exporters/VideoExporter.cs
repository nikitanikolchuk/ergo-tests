using System.Globalization;
using System.IO;
using System.Text;
using TestAdministration.Models.Data;
using TestAdministration.Models.Storages.FileSystems;

namespace TestAdministration.Models.Storages.Exporters;

public class VideoExporter(
    IFileSystem fileSystem
)
{
    private const string DirectoryName = "Videozaznamy";
    private const char FirstLetter = 'a';

    /// <summary>
    /// Copy a list of existing files to the test result
    /// directory. If there is more than one file, appends
    /// letters to distinguish them. Does not replace existing
    /// files.
    /// </summary>
    public void CopyFiles(Patient patient, Test test, List<string> filePaths)
    {
        var directoryPath = Path.Combine(fileSystem.TestDataPath, _patientDirectoryName(patient), DirectoryName);
        Directory.CreateDirectory(directoryPath);

        List<string> targetFilePaths;
        if (filePaths.Count == 1)
        {
            var fileName = _getFileName(patient, test, filePaths.First());
            targetFilePaths = [Path.Combine(directoryPath, fileName)];
        }
        else
        {
            targetFilePaths = filePaths.Select((path, i) =>
                {
                    var fileName = _getFileName(patient, test, path, (char)(FirstLetter + i));
                    return Path.Combine(directoryPath, fileName);
                }
            ).ToList();
        }

        for (var i = 0; i < filePaths.Count; i++)
        {
            if (!File.Exists(targetFilePaths[i]))
            {
                File.Copy(filePaths[i], targetFilePaths[i]);
            }
        }
    }

    private static string _patientDirectoryName(Patient patient)
    {
        var surname = patient.Surname.ToUpper().Replace(" ", "-");
        var name = patient.Name.ToUpper().Replace(" ", "-");
        var id = patient.Id.Replace(" ", "-");
        return _removeDiacritics($"{surname}_{name}_{id}");
    }

    private static string _getFileName(Patient patient, Test test, string filePath, char? letter = null)
    {
        var surname = patient.Surname.ToUpper().Replace(" ", "-");
        var name = patient.Name.ToUpper().Replace(" ", "-");
        var testType = test.Type.ToString().ToUpper();
        var timestamp = test.Date.ToDateTime(test.StartTime).ToString("yyyy-MM-dd_HH-mm-ss");
        var letterSuffix = letter is not null ? $"_{letter}" : string.Empty;
        var fileExtension = Path.GetExtension(filePath);

        return _removeDiacritics($"{surname}_{name}_{testType}_{timestamp}{letterSuffix}{fileExtension}");
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