using System.Globalization;
using System.Windows.Data;
using TestAdministration.ViewModels.Rules;

namespace TestAdministration.Views.Rules;

/// <summary>
/// A class for extracting the first value of a row in a
/// Purdue Pegboard Test rules table.
/// </summary>
[ValueConversion(typeof(string),typeof(PptRuleRow))]
public class PptRuleRowConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string situation)
        {
            return new PptRuleRow(situation, string.Empty, string.Empty);
        }

        return null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is PptRuleRow row)
        {
            return row.Situation;
        }

        return null;
    }
}