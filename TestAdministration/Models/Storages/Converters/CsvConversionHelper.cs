using System.ComponentModel;
using System.Text.RegularExpressions;
using TestAdministration.Models.Data;

namespace TestAdministration.Models.Storages.Converters;

/// <summary>
/// A static helper class for CSV conversion.
/// </summary>
public static partial class CsvConversionHelper
{
    private static readonly List<string> ShortNoteNamesList =
    [
        "Dom. zkuš. pokus",
        "Dom. 1. pokus",
        "Dom. 2. pokus",
        "Dom. 3. pokus",
        "Nedom. zkuš. pokus",
        "Nedom. 1. pokus",
        "Nedom. 2. pokus",
        "Nedom. 3. pokus"
    ];

    private static readonly List<string> LongNoteNamesList =
    [
        "Dom. 1. pokus",
        "Dom. 2. pokus",
        "Dom. 3. pokus",
        "Nedom. 1. pokus",
        "Nedom. 2. pokus",
        "Nedom. 3. pokus",
        "Obě HK 1. pokus",
        "Obě HK 2. pokus",
        "Obě HK 3. pokus",
        string.Empty,
        string.Empty,
        string.Empty,
        "Kompletování 1. pokus",
        "Kompletování 2. pokus",
        "Kompletování 3. pokus",
    ];

    [GeneratedRegex(@"\s+")]
    private static partial Regex WhitespaceRegex();

    /// <summary>
    /// Creates a single string with named notes from individual
    /// trial notes. Replaces multiple whitespace characters with
    /// single spaces.
    /// </summary>
    /// <param name="test">The test to take notes from.</param>
    public static string CreateNotes(Test test)
    {
        var noteNames = _getNoteNames(test.Type);
        var notes = test.Sections
            .SelectMany(s => s.Trials.Select(t => t.Note))
            .Select(note => WhitespaceRegex().Replace(note, " "))
            .Select((note, i) =>
                !string.IsNullOrWhiteSpace(note) && noteNames[i] != string.Empty
                    ? $"{noteNames[i]}: {note}"
                    : string.Empty
            )
            .Where(note => note != string.Empty);
        return string.Join('\n', notes);
    }

    /// <summary>
    /// Parses a string into a list of separate notes using
    /// a list of line prefixes. Empty prefixes are ignored.
    /// </summary>
    /// <param name="notes">The string containing all the notes.</param>
    /// <param name="noteNames">Note prefixes to be found on each line.</param>
    public static IList<string> ParseNotes(string notes, IList<string> noteNames)
    {
        var result = new List<string>(new string[noteNames.Count]);
        var lines = notes.Split('\n');
        for (var i = 0; i < noteNames.Count; i++)
        {
            if (noteNames[i] == string.Empty)
            {
                continue;
            }

            foreach (var row in lines)
            {
                if (!row.StartsWith(noteNames[i]))
                {
                    continue;
                }

                result[i] = row[(noteNames[i].Length + 1)..];
                break;
            }
        }

        return result;
    }

    private static List<string> _getNoteNames(TestType testType) => testType switch
    {
        TestType.Nhpt => ShortNoteNamesList,
        TestType.Ppt => LongNoteNamesList,
        TestType.Bbt => ShortNoteNamesList,
        _ => throw new InvalidEnumArgumentException(
            nameof(testType),
            Convert.ToInt32(testType),
            typeof(TestType)
        )
    };
}