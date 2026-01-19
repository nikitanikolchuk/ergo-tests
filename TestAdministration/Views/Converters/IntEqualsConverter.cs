using System.Globalization;
using System.Windows.Data;

namespace TestAdministration.Views.Converters;

/// <summary>
/// A converter for checking if the string parameter
/// is equal to the expected int value.
/// </summary>
[ValueConversion(typeof(string), typeof(bool))]
public sealed class IntEqualsConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not int current || !int.TryParse(parameter?.ToString(), out var expected))
        {
            return false;
        }
        
        return current == expected;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is true && int.TryParse(parameter?.ToString(), out var chosen))
        {
            return chosen;
        }

        return Binding.DoNothing;
    }
}