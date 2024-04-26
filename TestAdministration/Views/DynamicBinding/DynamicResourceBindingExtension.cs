using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace TestAdministration.Views.DynamicBinding;

/// <summary>
/// A class for defining binding for dynamic resources.
/// <see href="https://stackoverflow.com/questions/33816511/how-can-you-bind-to-a-dynamicresource-so-you-can-use-a-converter-or-stringformat"/>
/// </summary>
public class DynamicResourceBindingExtension(object resourceKey) : MarkupExtension
{
    private BindingProxy? _bindingProxy;
    private BindingTrigger? _bindingTrigger;

    private object ResourceKey => resourceKey ?? throw new ArgumentNullException(nameof(resourceKey));
    public IValueConverter? Converter { get; set; }
    public object? ConverterParameter { get; set; }
    public CultureInfo? ConverterCulture { get; }
    public string? StringFormat { get; }
    public object? TargetNullValue { get; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        var dynamicResource = new DynamicResourceExtension(ResourceKey);
        _bindingProxy = new BindingProxy(dynamicResource.ProvideValue(null));

        var dynamicResourceBinding = new Binding
        {
            Source = _bindingProxy,
            Path = new PropertyPath(BindingProxy.ValueProperty),
            Mode = BindingMode.OneWay
        };

        var targetInfoService = serviceProvider.GetService(typeof(IProvideValueTarget));
        if (targetInfoService is null)
        {
            throw new InvalidOperationException($"Missing {typeof(IProvideValueTarget)} service");
        }

        var targetInfo = (IProvideValueTarget)targetInfoService;

        if (targetInfo.TargetObject is DependencyObject dependencyObject)
        {
            dynamicResourceBinding.Converter = Converter;
            dynamicResourceBinding.ConverterParameter = ConverterParameter;
            dynamicResourceBinding.ConverterCulture = ConverterCulture;
            dynamicResourceBinding.StringFormat = StringFormat;
            dynamicResourceBinding.TargetNullValue = TargetNullValue;

            if (dependencyObject is FrameworkElement targetFrameworkElement)
            {
                targetFrameworkElement.Resources[_bindingProxy] = _bindingProxy;
            }

            return dynamicResourceBinding.ProvideValue(serviceProvider);
        }

        var findTargetBinding = new Binding
        {
            RelativeSource = new RelativeSource(RelativeSourceMode.Self)
        };
        _bindingTrigger = new BindingTrigger();
        var wrapperBinding = new MultiBinding
        {
            Bindings =
            {
                dynamicResourceBinding,
                findTargetBinding,
                _bindingTrigger.Binding
            },
            Converter = new InlineMultiConverter(WrapperConvert)
        };

        return wrapperBinding.ProvideValue(serviceProvider);
    }

    private object? WrapperConvert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        var dynamicResourceBindingResult = values[0];
        var bindingTargetObject = values[1];

        if (Converter is not null)
        {
            dynamicResourceBindingResult = (object?)Converter.Convert(
                dynamicResourceBindingResult,
                targetType,
                ConverterParameter,
                ConverterCulture
            );
        }

        if (dynamicResourceBindingResult is null)
        {
            dynamicResourceBindingResult = TargetNullValue;
        }
        else if (targetType == typeof(string) && StringFormat is not null)
        {
            dynamicResourceBindingResult = string.Format(StringFormat, dynamicResourceBindingResult);
        }

        if (bindingTargetObject is not FrameworkElement targetFrameworkElement
            || targetFrameworkElement.Resources.Contains(_bindingProxy!))
        {
            return dynamicResourceBindingResult;
        }

        targetFrameworkElement.Resources[_bindingProxy!] = _bindingProxy;
        SynchronizationContext.Current?.Post(
            _ => _bindingTrigger!.Refresh(),
            null
        );

        return dynamicResourceBindingResult;
    }
}