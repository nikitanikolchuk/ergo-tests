using System.Globalization;
using System.Windows.Data;

namespace TestAdministration.Views.Converters;

/// <summary>
/// A class for specifying width of a table cell that is slightly
/// narrower than the parent container width divided by number of
/// cells in a row.
/// </summary>
[ValueConversion(typeof(double), typeof(double))]
public class TableRowWidthConverter : IValueConverter
{
    private const double Difference = 30;

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var cells = 1;
        if (parameter is string cellCount)
        {
            _ = int.TryParse(cellCount, out cells);
        }

        if (value is double width)
        {
            return (width - Difference) / cells;
        }

        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var cells = 1;
        if (parameter is string cellCount)
        {
            _ = int.TryParse(cellCount, out cells);
        }

        if (value is double width)
        {
            return width * cells;
        }

        return value;
    }
}