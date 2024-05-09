using System.Globalization;

namespace TestAdministration.ViewModels.Results;

/// <summary>
/// A class for providing bindings to a single row of results
/// table for <see cref="ResultTableViewModel"/>
/// </summary>
public class ResultTableRow(
    string header,
    float? value,
    float? sdScore,
    string normInterpretation,
    float? previousValue,
    float? previousSdScore,
    string previousNormInterpretation
)
{
    private const string FloatFormat = "0.##";
    private const string Culture = "cs";

    public string Header => header;
    public string Value => _floatToString(value);
    public string SdScore => _floatToString(sdScore);
    public string NormInterpretation => normInterpretation;
    public string PreviousValue => $" ({_floatToString(previousValue)})";
    public string PreviousSdScore => $" ({_floatToString(previousSdScore)})";
    public string PreviousNormInterpretation => $" ({previousNormInterpretation})";

    private static string _floatToString(float? number) =>
        number is not null
            ? number.Value.ToString(FloatFormat, new CultureInfo(Culture))
            : string.Empty;
}