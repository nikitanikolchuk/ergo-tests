using System.Windows;

namespace TestAdministration.Views;

public partial class NavButton
{
    public event RoutedEventHandler? Click;

    private static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(NavButton),
            new PropertyMetadata(string.Empty)
        );
    
    public NavButton()
    {
        InitializeComponent();
    }
    
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    
    private void OnClick(object sender, RoutedEventArgs e)
    {
        Click?.Invoke(this, e);
    }
}