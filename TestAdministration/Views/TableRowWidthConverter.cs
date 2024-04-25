using System.Globalization;
using System.Windows.Data;

namespace TestAdministration.Views;

/// <summary>
/// A class for specifying width of a table cell that is slightly
/// narrower than the parent container width divided by number of
/// cells in a row.
/// </summary>
[ValueConversion(typeof(double), typeof(double))]
public class TableRowWidthConverter : IValueConverter
{
    private const double Difference = 16;
    private const double RowCount = 3;

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double width)
        {
            return (width - Difference) / RowCount;
        }

        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double width)
        {
            return width * RowCount;
        }

        return value;
    }
}