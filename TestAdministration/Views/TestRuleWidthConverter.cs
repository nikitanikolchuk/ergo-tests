using System.Globalization;
using System.Windows.Data;

namespace TestAdministration.Views;

/// <summary>
/// A class for specifying width of a test rule UI item that is
/// slightly narrower than the parent container.
/// </summary>
[ValueConversion(typeof(double),typeof(double))]
public class TestRuleWidthConverter : IValueConverter
{
    private const double Difference = 16;
    
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double width)
        {
            return width - Difference;
        }

        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double width)
        {
            return width + Difference;
        }

        return value;
    }
}