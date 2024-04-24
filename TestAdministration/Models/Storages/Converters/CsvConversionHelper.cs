using System.Text.RegularExpressions;
using TestAdministration.Models.Data;

namespace TestAdministration.Models.Storages.Converters;

/// <summary>
/// A static helper class for CSV conversion.
/// </summary>
public static partial class CsvConversionHelper
{
    [GeneratedRegex(@"\s+")]
    private static partial Regex WhitespaceRegex();

    /// <summary>
    /// Creates a single string with named notes from individual
    /// trial notes. Replaces multiple whitespace characters with
    /// single spaces.
    /// </summary>
    /// <param name="test">The test to take notes from.</param>
    /// <param name="noteNames">
    /// A list of note names of the same length as the test's
    /// trial count.
    /// </param>
    public static string CreateNotes(Test test, IList<string> noteNames)
    {
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
}