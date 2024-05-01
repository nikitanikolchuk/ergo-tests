using TestAdministration.Models.Data;

namespace TestAdministration.ViewModels.Results;

/// <summary>
/// A view model for displaying results of a single test section
/// with optional comparison to the previous values.
/// </summary>
public class ResultTableViewModel(
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
            trial.NormDifference,
            previousTestSection?.Trials[i].Value,
            previousTestSection?.Trials[i].SdScore,
            previousTestSection?.Trials[i].NormDifference
        )).ToList();

        var averagesRow = new ResultTableRow(
            "Průměr",
            testSection.AverageValue,
            testSection.AverageSdScore,
            testSection.AverageNormDifference,
            previousTestSection?.AverageValue,
            previousTestSection?.AverageSdScore,
            previousTestSection?.AverageNormDifference
        );

        return rows.Append(averagesRow).ToList();
    }
}