using System.Globalization;
using System.Windows.Data;

namespace TestAdministration.Views.DynamicBinding;

/// <summary>
/// A helper type for using <see cref="IMultiValueConverter"/>
/// objects without explicit creation of new types through delegates.
/// </summary>
public class InlineMultiConverter(
    InlineMultiConverter.ConvertDelegate? convert,
    InlineMultiConverter.ConvertBackDelegate? convertBackProperty = null
) : IMultiValueConverter
{
    public delegate object? ConvertDelegate(
        object[] values,
        Type targetType,
        object parameter,
        CultureInfo culture
    );

    public delegate object[]? ConvertBackDelegate(
        object value,
        Type[] targetTypes,
        object parameter,
        CultureInfo culture
    );

    private ConvertDelegate ConvertProperty => convert ?? throw new ArgumentNullException(nameof(convert));
    private ConvertBackDelegate? ConvertBackProperty => convertBackProperty;

    public object? Convert(object[] values, Type targetType, object parameter, CultureInfo culture) =>
        ConvertProperty(values, targetType, parameter, culture);

    public object[]? ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) =>
        ConvertBackProperty != null
            ? ConvertBackProperty(value, targetTypes, parameter, culture)
            : throw new NotImplementedException();
}