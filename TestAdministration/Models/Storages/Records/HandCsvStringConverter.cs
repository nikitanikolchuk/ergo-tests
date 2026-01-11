using System.ComponentModel;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using TestAdministration.Models.Data;
using TestAdministration.Models.Storages.Converters;

namespace TestAdministration.Models.Storages.Records;

/// <summary>
/// A class for string conversion of <see cref="Hand"/> enum.
/// </summary>
public class HandCsvStringConverter : ITypeConverter
{
    private const string None = "žádná";
    private const string Left = "levá";
    private const string Right = "pravá";
    private const string Both = "obě";

    public string ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
    {
        if (value is not Hand hand)
        {
            throw new ArgumentException("HandCsvStringConverter used not for Hand enum");
        }

        return hand switch
        {
            Hand.None => None,
            Hand.Left => Left,
            Hand.Right => Right,
            Hand.Both => Both,
            _ => throw new InvalidEnumArgumentException(
                nameof(hand),
                Convert.ToInt32(hand),
                typeof(Hand)
            )
        };
    }

    public object ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData) =>
        text switch
        {
            None => Hand.None,
            Left => Hand.Left,
            Right => Hand.Right,
            Both => Hand.Both,
            _ => throw new CsvConverterException($"Invalid Hand string value: {text}")
        };
}