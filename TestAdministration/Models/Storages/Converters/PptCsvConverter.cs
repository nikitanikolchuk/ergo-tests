using TestAdministration.Models.Data;
using TestAdministration.Models.Storages.Records;
using static TestAdministration.Models.Storages.Converters.CsvConversionHelper;

namespace TestAdministration.Models.Storages.Converters;

/// <summary>
/// A class for conversion between <see cref="Test"/> and
/// <see cref="PptCsvRecord"/> objects.
/// </summary>
public class PptCsvConverter
{
    public Test FromRecord(PptCsvRecord record)
    {
        var notes = ParseNotes(record.Notes, PptCsvRecord.NoteNames);
        return _fromRecord(record, notes);
    }

    public PptCsvRecord ToRecord(Test test) => new()
    {
        Tester = test.Tester,
        Date = test.Date,
        StartTime = test.StartTime,
        EndTime = test.EndTime,
        DominantFirst = test.Sections[0].Trials[0].Value,
        DominantSecond = test.Sections[0].Trials[1].Value,
        DominantThird = test.Sections[0].Trials[2].Value,
        DominantAverage = test.Sections[0].AverageValue,
        NonDominantFirst = test.Sections[1].Trials[0].Value,
        NonDominantSecond = test.Sections[1].Trials[1].Value,
        NonDominantThird = test.Sections[1].Trials[2].Value,
        NonDominantAverage = test.Sections[1].AverageValue,
        BothFirst = test.Sections[2].Trials[0].Value,
        BothSecond = test.Sections[2].Trials[1].Value,
        BothThird = test.Sections[2].Trials[2].Value,
        BothAverage = test.Sections[2].AverageValue,
        TotalFirst = test.Sections[3].Trials[0].Value,
        TotalSecond = test.Sections[3].Trials[1].Value,
        TotalThird = test.Sections[3].Trials[2].Value,
        TotalAverage = test.Sections[3].AverageValue,
        AssemblyFirst = test.Sections[4].Trials[0].Value,
        AssemblySecond = test.Sections[4].Trials[1].Value,
        AssemblyThird = test.Sections[4].Trials[2].Value,
        AssemblyAverage = test.Sections[4].AverageValue,
        DominantFirstSdScore = test.Sections[0].Trials[0].SdScore,
        DominantSecondSdScore = test.Sections[0].Trials[1].SdScore,
        DominantThirdSdScore = test.Sections[0].Trials[2].SdScore,
        DominantAverageSdScore = test.Sections[0].AverageSdScore,
        NonDominantFirstSdScore = test.Sections[1].Trials[0].SdScore,
        NonDominantSecondSdScore = test.Sections[1].Trials[1].SdScore,
        NonDominantThirdSdScore = test.Sections[1].Trials[2].SdScore,
        NonDominantAverageSdScore = test.Sections[1].AverageSdScore,
        BothFirstSdScore = test.Sections[2].Trials[0].SdScore,
        BothSecondSdScore = test.Sections[2].Trials[1].SdScore,
        BothThirdSdScore = test.Sections[2].Trials[2].SdScore,
        BothAverageSdScore = test.Sections[2].AverageSdScore,
        TotalFirstSdScore = test.Sections[3].Trials[0].SdScore,
        TotalSecondSdScore = test.Sections[3].Trials[1].SdScore,
        TotalThirdSdScore = test.Sections[3].Trials[2].SdScore,
        TotalAverageSdScore = test.Sections[3].AverageSdScore,
        AssemblyFirstSdScore = test.Sections[4].Trials[0].SdScore,
        AssemblySecondSdScore = test.Sections[4].Trials[1].SdScore,
        AssemblyThirdSdScore = test.Sections[4].Trials[2].SdScore,
        AssemblyAverageSdScore = test.Sections[4].AverageSdScore,
        DominantFirstNormDifference = test.Sections[0].Trials[0].NormDifference,
        DominantSecondNormDifference = test.Sections[0].Trials[1].NormDifference,
        DominantThirdNormDifference = test.Sections[0].Trials[2].NormDifference,
        DominantAverageNormDifference = test.Sections[0].AverageNormDifference,
        NonDominantFirstNormDifference = test.Sections[1].Trials[0].NormDifference,
        NonDominantSecondNormDifference = test.Sections[1].Trials[1].NormDifference,
        NonDominantThirdNormDifference = test.Sections[1].Trials[2].NormDifference,
        NonDominantAverageNormDifference = test.Sections[1].AverageNormDifference,
        BothFirstNormDifference = test.Sections[2].Trials[0].NormDifference,
        BothSecondNormDifference = test.Sections[2].Trials[1].NormDifference,
        BothThirdNormDifference = test.Sections[2].Trials[2].NormDifference,
        BothAverageNormDifference = test.Sections[2].AverageNormDifference,
        TotalFirstNormDifference = test.Sections[3].Trials[0].NormDifference,
        TotalSecondNormDifference = test.Sections[3].Trials[1].NormDifference,
        TotalThirdNormDifference = test.Sections[3].Trials[2].NormDifference,
        TotalAverageNormDifference = test.Sections[3].AverageNormDifference,
        AssemblyFirstNormDifference = test.Sections[4].Trials[0].NormDifference,
        AssemblySecondNormDifference = test.Sections[4].Trials[1].NormDifference,
        AssemblyThirdNormDifference = test.Sections[4].Trials[2].NormDifference,
        AssemblyAverageNormDifference = test.Sections[4].AverageNormDifference,
        Notes = CreateNotes(test)
    };

    private static Test _fromRecord(PptCsvRecord record, IList<string> notes) => new(
        TestType.Ppt,
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
                        record.DominantFirst,
                        record.DominantFirstSdScore,
                        record.DominantFirstNormDifference,
                        notes[0]
                    ),
                    new TestTrial(
                        record.DominantSecond,
                        record.DominantSecondSdScore,
                        record.DominantSecondNormDifference,
                        notes[1]
                    ),
                    new TestTrial(
                        record.DominantThird,
                        record.DominantThirdSdScore,
                        record.DominantThirdNormDifference,
                        notes[2]
                    )
                ]
            ),
            new TestSection(
                record.NonDominantAverage,
                record.NonDominantAverageSdScore,
                record.NonDominantAverageNormDifference,
                [
                    new TestTrial(
                        record.NonDominantFirst,
                        record.NonDominantFirstSdScore,
                        record.NonDominantFirstNormDifference,
                        notes[3]
                    ),
                    new TestTrial(
                        record.NonDominantSecond,
                        record.NonDominantSecondSdScore,
                        record.NonDominantSecondNormDifference,
                        notes[4]
                    ),
                    new TestTrial(
                        record.NonDominantThird,
                        record.NonDominantThirdSdScore,
                        record.NonDominantThirdNormDifference,
                        notes[5]
                    )
                ]
            ),
            new TestSection(
                record.BothAverage,
                record.BothAverageSdScore,
                record.BothAverageNormDifference,
                [
                    new TestTrial(
                        record.BothFirst,
                        record.BothFirstSdScore,
                        record.BothFirstNormDifference,
                        notes[6]
                    ),
                    new TestTrial(
                        record.BothSecond,
                        record.BothSecondSdScore,
                        record.BothSecondNormDifference,
                        notes[7]
                    ),
                    new TestTrial(
                        record.BothThird,
                        record.BothThirdSdScore,
                        record.BothThirdNormDifference,
                        notes[8]
                    )
                ]
            ),
            new TestSection(
                record.TotalAverage,
                record.TotalAverageSdScore,
                record.TotalAverageNormDifference,
                [
                    new TestTrial(
                        record.TotalFirst,
                        record.TotalFirstSdScore,
                        record.TotalFirstNormDifference,
                        string.Empty
                    ),
                    new TestTrial(
                        record.TotalSecond,
                        record.TotalSecondSdScore,
                        record.TotalSecondNormDifference,
                        string.Empty
                    ),
                    new TestTrial(
                        record.TotalThird,
                        record.TotalThirdSdScore,
                        record.TotalThirdNormDifference,
                        string.Empty
                    )
                ]
            ),
            new TestSection(
                record.AssemblyAverage,
                record.AssemblyAverageSdScore,
                record.AssemblyAverageNormDifference,
                [
                    new TestTrial(
                        record.AssemblyFirst,
                        record.AssemblyFirstSdScore,
                        record.AssemblyFirstNormDifference,
                        notes[12]
                    ),
                    new TestTrial(
                        record.AssemblySecond,
                        record.AssemblySecondSdScore,
                        record.AssemblySecondNormDifference,
                        notes[13]
                    ),
                    new TestTrial(
                        record.AssemblyThird,
                        record.AssemblyThirdSdScore,
                        record.AssemblyThirdNormDifference,
                        notes[14]
                    )
                ]
            )
        ]
    );
}