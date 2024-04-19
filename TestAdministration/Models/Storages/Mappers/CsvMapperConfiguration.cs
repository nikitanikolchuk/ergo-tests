using System.Globalization;

namespace TestAdministration.Models.Storages.Mappers;

/// <summary>
/// A helper class that defines CSV data format.
/// </summary>
public static class CsvMapperConfiguration
{
    public const string DateFormat = "dd.MM.yyyy";
    public const string TimeFormat = "HH:mm";

    private const string FloatFormat = "0.##";

    public static string FormatValue(float? value) =>
        value != null
            ? value.Value.ToString(FloatFormat, CultureInfo.GetCultureInfo("cz"))
            : "";
}