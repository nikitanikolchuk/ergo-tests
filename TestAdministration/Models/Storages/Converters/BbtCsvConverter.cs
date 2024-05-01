using TestAdministration.Models.Data;
using TestAdministration.Models.Storages.Records;
using static TestAdministration.Models.Storages.Converters.CsvConversionHelper;

namespace TestAdministration.Models.Storages.Converters;

/// <summary>
/// A class for conversion between <see cref="Test"/> and
/// <see cref="BbtCsvRecord"/> objects.
/// </summary>
public class BbtCsvConverter
{
    public Test FromRecord(BbtCsvRecord record)
    {
        var notes = ParseNotes(record.Notes, BbtCsvRecord.NoteNames);
        return _fromRecord(record, notes);
    }

    public BbtCsvRecord ToRecord(Test test) => new()
    {
        Tester = test.Tester,
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
        DominantPracticeNormDifference = test.Sections[0].Trials[0].NormDifference,
        DominantFirstNormDifference = test.Sections[0].Trials[1].NormDifference,
        DominantSecondNormDifference = test.Sections[0].Trials[2].NormDifference,
        DominantThirdNormDifference = test.Sections[0].Trials[3].NormDifference,
        DominantAverageNormDifference = test.Sections[0].AverageNormDifference,
        NonDominantPracticeNormDifference = test.Sections[1].Trials[0].NormDifference,
        NonDominantFirstNormDifference = test.Sections[1].Trials[1].NormDifference,
        NonDominantSecondNormDifference = test.Sections[1].Trials[2].NormDifference,
        NonDominantThirdNormDifference = test.Sections[1].Trials[3].NormDifference,
        NonDominantAverageNormDifference = test.Sections[1].AverageNormDifference,
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
                record.DominantAverageNormDifference,
                [
                    new TestTrial(
                        record.DominantPractice,
                        record.DominantPracticeSdScore,
                        record.DominantPracticeNormDifference,
                        notes[0]
                    ),
                    new TestTrial(
                        record.DominantFirst,
                        record.DominantFirstSdScore,
                        record.DominantFirstNormDifference,
                        notes[1]
                    ),
                    new TestTrial(
                        record.DominantSecond,
                        record.DominantSecondSdScore,
                        record.DominantSecondNormDifference,
                        notes[2]
                    ),
                    new TestTrial(
                        record.DominantThird,
                        record.DominantThirdSdScore,
                        record.DominantThirdNormDifference,
                        notes[3]
                    )
                ]
            ),
            new TestSection(
                record.NonDominantAverage,
                record.NonDominantAverageSdScore,
                record.NonDominantAverageNormDifference,
                [
                    new TestTrial(
                        record.NonDominantPractice,
                        record.NonDominantPracticeSdScore,
                        record.NonDominantPracticeNormDifference,
                        notes[4]
                    ),
                    new TestTrial(
                        record.NonDominantFirst,
                        record.NonDominantFirstSdScore,
                        record.NonDominantFirstNormDifference,
                        notes[5]
                    ),
                    new TestTrial(
                        record.NonDominantSecond,
                        record.NonDominantSecondSdScore,
                        record.NonDominantSecondNormDifference,
                        notes[6]
                    ),
                    new TestTrial(
                        record.NonDominantThird,
                        record.NonDominantThirdSdScore,
                        record.NonDominantThirdNormDifference,
                        notes[7]
                    )
                ]
            )
        ]
    );
}