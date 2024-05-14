using TestAdministration.Models.Data;
using TestAdministration.Models.Storages.Records;
using static TestAdministration.Models.Storages.Converters.CsvConversionHelper;

namespace TestAdministration.Models.Storages.Converters;

/// <summary>
/// A class for conversion between <see cref="Test"/> and
/// <see cref="PptCsvRecord"/> objects.
/// </summary>
public class PptCsvConverter(
    NormInterpretationConverter normInterpretationConverter
)
{
    public Test FromRecord(PptCsvRecord record)
    {
        var notes = ParseNotes(record.Notes, PptCsvRecord.NoteNames);
        return _fromRecord(record, notes);
    }

    public PptCsvRecord ToRecord(Patient patient, Test test) => new()
    {
        Tester = test.Tester,
        Id = patient.Id,
        Name = patient.Name,
        Surname = patient.Surname,
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
        DominantFirstNormInterpretation = normInterpretationConverter.ConvertFull(test.Sections[0].Trials[0].SdScore),
        DominantSecondNormInterpretation = normInterpretationConverter.ConvertFull(test.Sections[0].Trials[1].SdScore),
        DominantThirdNormInterpretation = normInterpretationConverter.ConvertFull(test.Sections[0].Trials[2].SdScore),
        DominantAverageNormInterpretation = normInterpretationConverter.ConvertFull(test.Sections[0].AverageSdScore),
        NonDominantFirstNormInterpretation = normInterpretationConverter.ConvertFull(test.Sections[1].Trials[0].SdScore),
        NonDominantSecondNormInterpretation = normInterpretationConverter.ConvertFull(test.Sections[1].Trials[1].SdScore),
        NonDominantThirdNormInterpretation = normInterpretationConverter.ConvertFull(test.Sections[1].Trials[2].SdScore),
        NonDominantAverageNormInterpretation = normInterpretationConverter.ConvertFull(test.Sections[1].AverageSdScore),
        BothFirstNormInterpretation = normInterpretationConverter.ConvertFull(test.Sections[2].Trials[0].SdScore),
        BothSecondNormInterpretation = normInterpretationConverter.ConvertFull(test.Sections[2].Trials[1].SdScore),
        BothThirdNormInterpretation = normInterpretationConverter.ConvertFull(test.Sections[2].Trials[2].SdScore),
        BothAverageNormInterpretation = normInterpretationConverter.ConvertFull(test.Sections[2].AverageSdScore),
        TotalFirstNormInterpretation = normInterpretationConverter.ConvertFull(test.Sections[3].Trials[0].SdScore),
        TotalSecondNormInterpretation = normInterpretationConverter.ConvertFull(test.Sections[3].Trials[1].SdScore),
        TotalThirdNormInterpretation = normInterpretationConverter.ConvertFull(test.Sections[3].Trials[2].SdScore),
        TotalAverageNormInterpretation = normInterpretationConverter.ConvertFull(test.Sections[3].AverageSdScore),
        AssemblyFirstNormInterpretation = normInterpretationConverter.ConvertFull(test.Sections[4].Trials[0].SdScore),
        AssemblySecondNormInterpretation = normInterpretationConverter.ConvertFull(test.Sections[4].Trials[1].SdScore),
        AssemblyThirdNormInterpretation = normInterpretationConverter.ConvertFull(test.Sections[4].Trials[2].SdScore),
        AssemblyAverageNormInterpretation = normInterpretationConverter.ConvertFull(test.Sections[4].AverageSdScore),
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
                [
                    new TestTrial(record.DominantFirst, record.DominantFirstSdScore, notes[0]),
                    new TestTrial(record.DominantSecond, record.DominantSecondSdScore, notes[1]),
                    new TestTrial(record.DominantThird, record.DominantThirdSdScore, notes[2])
                ]
            ),
            new TestSection(
                record.NonDominantAverage,
                record.NonDominantAverageSdScore,
                [
                    new TestTrial(record.NonDominantFirst, record.NonDominantFirstSdScore, notes[3]),
                    new TestTrial(record.NonDominantSecond, record.NonDominantSecondSdScore, notes[4]),
                    new TestTrial(record.NonDominantThird, record.NonDominantThirdSdScore, notes[5])
                ]
            ),
            new TestSection(
                record.BothAverage,
                record.BothAverageSdScore,
                [
                    new TestTrial(record.BothFirst, record.BothFirstSdScore, notes[6]),
                    new TestTrial(record.BothSecond, record.BothSecondSdScore, notes[7]),
                    new TestTrial(record.BothThird, record.BothThirdSdScore, notes[8])
                ]
            ),
            new TestSection(
                record.TotalAverage,
                record.TotalAverageSdScore,
                [
                    new TestTrial(record.TotalFirst, record.TotalFirstSdScore, string.Empty),
                    new TestTrial(record.TotalSecond, record.TotalSecondSdScore, string.Empty),
                    new TestTrial(record.TotalThird, record.TotalThirdSdScore, string.Empty)
                ]
            ),
            new TestSection(
                record.AssemblyAverage,
                record.AssemblyAverageSdScore,
                [
                    new TestTrial(record.AssemblyFirst, record.AssemblyFirstSdScore, notes[12]),
                    new TestTrial(record.AssemblySecond, record.AssemblySecondSdScore, notes[13]),
                    new TestTrial(record.AssemblyThird, record.AssemblyThirdSdScore, notes[14])
                ]
            )
        ]
    );
}