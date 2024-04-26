using System.Windows;

namespace TestAdministration.Views.DynamicBinding;

/// <summary>
/// A proxy class for binding to a dynamic resource.
/// </summary>
public class BindingProxy : Freezable
{
    public BindingProxy()
    {
    }

    public BindingProxy(object value) => Value = value;

    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(
            nameof(Value),
            typeof(object),
            typeof(BindingProxy),
            new FrameworkPropertyMetadata(default)
        );

    public object Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    protected override Freezable CreateInstanceCore() => new BindingProxy();
}