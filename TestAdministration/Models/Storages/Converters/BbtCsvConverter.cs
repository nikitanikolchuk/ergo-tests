using TestAdministration.Models.Data;
using TestAdministration.Models.Storages.Records;
using static TestAdministration.Models.Storages.Converters.CsvConversionHelper;

namespace TestAdministration.Models.Storages.Converters;

/// <summary>
/// A class for conversion between <see cref="Test"/> and
/// <see cref="BbtCsvRecord"/> objects.
/// </summary>
public class BbtCsvConverter(
    NormInterpretationConverter normInterpretationConverter
)
{
    public Test FromRecord(BbtCsvRecord record)
    {
        var notes = ParseNotes(record.Notes, BbtCsvRecord.NoteNames);
        return _fromRecord(record, notes);
    }

    public BbtCsvRecord ToRecord(Patient patient, Test test) => new()
    {
        Tester = test.Tester,
        Id = patient.Id,
        Name = patient.Name,
        Surname = patient.Surname,
        Date = test.Date,
        StartTime = test.StartTime,
        EndTime = test.EndTime,
        DominantPractice = test.Sections[0].Trials[0].Value,
        DominantFirst = test.Sections[0].Trials[1].Value,
        DominantSecond = test.Sections[0].Trials[2].Value,
        DominantThird = test.Sections[0].Trials[3].Value,
        DominantAverage = test.Sections[0].AverageValue,
        NonDominantPractice = test.Sections[1].Trials[0].Value,
        NonDominantFirst = test.Sections[1].Trials[1].Value,
        NonDominantSecond = test.Sections[1].Trials[2].Value,
        NonDominantThird = test.Sections[1].Trials[3].Value,
        NonDominantAverage = test.Sections[1].AverageValue,
        DominantPracticeSdScore = test.Sections[0].Trials[0].SdScore,
        DominantFirstSdScore = test.Sections[0].Trials[1].SdScore,
        DominantSecondSdScore = test.Sections[0].Trials[2].SdScore,
        DominantThirdSdScore = test.Sections[0].Trials[3].SdScore,
        DominantAverageSdScore = test.Sections[0].AverageSdScore,
        NonDominantPracticeSdScore = test.Sections[1].Trials[0].SdScore,
        NonDominantFirstSdScore = test.Sections[1].Trials[1].SdScore,
        NonDominantSecondSdScore = test.Sections[1].Trials[2].SdScore,
        NonDominantThirdSdScore = test.Sections[1].Trials[3].SdScore,
        NonDominantAverageSdScore = test.Sections[1].AverageSdScore,
        DominantPracticeNormInterpretation = normInterpretationConverter.ConvertFull(test.Sections[0].Trials[0].SdScore),
        DominantFirstNormInterpretation = normInterpretationConverter.ConvertFull(test.Sections[0].Trials[1].SdScore),
        DominantSecondNormInterpretation = normInterpretationConverter.ConvertFull(test.Sections[0].Trials[2].SdScore),
        DominantThirdNormInterpretation = normInterpretationConverter.ConvertFull(test.Sections[0].Trials[3].SdScore),
        DominantAverageNormInterpretation = normInterpretationConverter.ConvertFull(test.Sections[0].AverageSdScore),
        NonDominantPracticeNormInterpretation = normInterpretationConverter.ConvertFull(test.Sections[1].Trials[0].SdScore),
        NonDominantFirstNormInterpretation = normInterpretationConverter.ConvertFull(test.Sections[1].Trials[1].SdScore),
        NonDominantSecondNormInterpretation = normInterpretationConverter.ConvertFull(test.Sections[1].Trials[2].SdScore),
        NonDominantThirdNormInterpretation = normInterpretationConverter.ConvertFull(test.Sections[1].Trials[3].SdScore),
        NonDominantAverageNormInterpretation = normInterpretationConverter.ConvertFull(test.Sections[1].AverageSdScore),
        Notes = CreateNotes(test)
    };

    private static Test _fromRecord(BbtCsvRecord record, IList<string> notes) => new(
        TestType.Bbt,
        record.Tester,
        record.Date,
        record.StartTime,
        record.EndTime,
        [
            new TestSection(
                record.DominantAverage,
                record.DominantAverageSdScore,
                [
                    new TestTrial(record.DominantPractice, record.DominantPracticeSdScore, notes[0]),
                    new TestTrial(record.DominantFirst, record.DominantFirstSdScore, notes[1]),
                    new TestTrial(record.DominantSecond, record.DominantSecondSdScore, notes[2]),
                    new TestTrial(record.DominantThird, record.DominantThirdSdScore, notes[3])
                ]
            ),
            new TestSection(
                record.NonDominantAverage,
                record.NonDominantAverageSdScore,
                [
                    new TestTrial(record.NonDominantPractice, record.NonDominantPracticeSdScore, notes[4]),
                    new TestTrial(record.NonDominantFirst, record.NonDominantFirstSdScore, notes[5]),
                    new TestTrial(record.NonDominantSecond, record.NonDominantSecondSdScore, notes[6]),
                    new TestTrial(record.NonDominantThird, record.NonDominantThirdSdScore, notes[7])
                ]
            )
        ]
    );
}