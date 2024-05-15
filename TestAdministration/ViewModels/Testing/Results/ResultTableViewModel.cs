using TestAdministration.Models.Data;
using TestAdministration.Models.Storages.Converters;

namespace TestAdministration.ViewModels.Testing.Results;

/// <summary>
/// A view model for displaying results of a single test section
/// with optional comparison to the previous values.
/// </summary>
public class ResultTableViewModel(
    NormInterpretationConverter normInterpretationConverter,
    TestSection testSection,
    TestSection? previousTestSection,
    string title,
    string valueType
) : ViewModelBase
{
    private static readonly List<string> LongHeaderList =
    [
        "Zkušební pokus",
        "1. pokus",
        "2. pokus",
        "3. pokus"
    ];

    private static readonly List<string> ShortHeaderList =
    [
        "1. pokus",
        "2. pokus",
        "3. pokus"
    ];

    public string Title => title;
    public string ValueType => valueType;
    public List<ResultTableRow> TableRows => _getTableRows();

    private List<ResultTableRow> _getTableRows()
    {
        var headerList = testSection.Trials.Count == LongHeaderList.Count
            ? LongHeaderList
            : ShortHeaderList;

        var rows = testSection.Trials.Select((trial, i) => new ResultTableRow(
            headerList[i],
            trial.Value,
            trial.SdScore,
            normInterpretationConverter.ConvertShort(trial.SdScore),
            previousTestSection?.Trials[i].Value,
            previousTestSection?.Trials[i].SdScore,
            normInterpretationConverter.ConvertShort(previousTestSection?.Trials[i].SdScore)
        )).ToList();

        var averagesRow = new ResultTableRow(
            "Průměr",
            testSection.AverageValue,
            testSection.AverageSdScore,
            normInterpretationConverter.ConvertShort(testSection.AverageSdScore),
            previousTestSection?.AverageValue,
            previousTestSection?.AverageSdScore,
            normInterpretationConverter.ConvertShort(previousTestSection?.AverageSdScore)
        );

        return rows.Append(averagesRow).ToList();
    }
}