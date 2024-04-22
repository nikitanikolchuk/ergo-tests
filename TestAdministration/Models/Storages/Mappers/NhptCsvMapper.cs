using System.Collections.Immutable;
using CsvHelper.Configuration;
using TestAdministration.Models.Data;
using static TestAdministration.Models.Storages.Mappers.CsvMapperConfiguration;

namespace TestAdministration.Models.Storages.Mappers;

/// <summary>
/// <see cref="ClassMap{Test}"/> implementation for Nine Hole Peg Test.
/// </summary>
public sealed class NhptCsvMapper : ClassMap<Test>
{
    private static readonly ImmutableList<string> NoteNames =
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

    public NhptCsvMapper()
    {
        Map(t => t.Tester)
            .Name("Testujici");
        Map(t => t.Date)
            .Name("Datum")
            .Convert(args => args.Value.Date.ToString(DateFormat));
        Map(t => t.StartTime)
            .Name("Cas_zahajeni")
            .Convert(args => args.Value.StartTime.ToString(TimeFormat));
        Map(t => t.EndTime)
            .Name("Cas_ukonceni")
            .Convert(args => args.Value.StartTime.ToString(TimeFormat));

        _mapSections();

        Map(t => t.Sections, false)
            .Name("Poznamky")
            .Convert(args => _createNotes(args.Value));
    }

    private static string _createNotes(Test test)
    {
        var notes = test.Sections
            .SelectMany(s => s.Trials.Select(t => t.Note))
            .Select((note, i) => !string.IsNullOrWhiteSpace(note) ? $"{NoteNames[i]}: {note}" : "")
            .Where(note => note != "");
        return string.Join('\n', notes);
    }

    private void _mapSections()
    {
        _mapSection(
            0,
            t => t.Value,
            s => s.AverageValue,
            "Dom",
            ""
        );
        _mapSection(
            1,
            t => t.Value,
            s => s.AverageValue,
            "Nedom",
            ""
        );
        _mapSection(
            0,
            t => t.SdScore,
            s => s.AverageSdScore,
            "Dom",
            "_SDS"
        );
        _mapSection(
            1,
            t => t.SdScore,
            s => s.AverageSdScore,
            "Nedom",
            "_SDS"
        );
        _mapSection(
            0,
            t => t.NormDifference,
            s => s.AverageNormDifference,
            "Dom",
            "_porovnani"
        );
        _mapSection(
            1,
            t => t.NormDifference,
            s => s.AverageNormDifference,
            "Nedom",
            "_porovnani"
        );
    }

    private void _mapSection(
        int section,
        Func<TestTrial, float?> trialValueSelector,
        Func<TestSection, float?> averageValueSelector,
        string namePrefix,
        string namePostfix
    )
    {
        _mapValue(
            $"{namePrefix}_zkus{namePostfix}",
            t => trialValueSelector(t.Sections[section].Trials[0])
        );
        _mapValue(
            $"{namePrefix}_1_pokus{namePostfix}",
            t => trialValueSelector(t.Sections[section].Trials[1])
        );
        _mapValue(
            $"{namePrefix}_2_pokus{namePostfix}",
            t => trialValueSelector(t.Sections[section].Trials[2])
        );
        _mapValue(
            $"{namePrefix}_3_pokus{namePostfix}",
            t => trialValueSelector(t.Sections[section].Trials[3])
        );
        _mapValue(
            $"{namePrefix}_prumer{namePostfix}",
            t => averageValueSelector(t.Sections[section])
        );
    }

    private void _mapValue(string name, Func<Test, float?> valueSelector)
    {
        Map(m => m.Sections, false)
            .Name(name)
            .Convert(args => FormatValue(valueSelector(args.Value)));
    }
}