using System.Globalization;
using System.Windows.Data;

namespace TestAdministration.Views.Converters;

/// <summary>
/// A converter for increasing the bound value by amount
/// specified by the parameter.
/// </summary>
[ValueConversion(typeof(double), typeof(double))]
public class AdditionConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var difference = 0;
        if (parameter is string differenceString)
        {
            _ = int.TryParse(differenceString, out difference);
        }

        if (value is double size)
        {
            return size + difference;
        }

        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var difference = 0;
        if (parameter is string differenceString)
        {
            _ = int.TryParse(differenceString, out difference);
        }

        if (value is double size)
        {
            return size - difference;
        }

        return value;
    }
}