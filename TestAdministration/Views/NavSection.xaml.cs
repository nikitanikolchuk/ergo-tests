using System.Windows;

namespace TestAdministration.Views;

public partial class NavSection
{
    private static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(
            nameof(Icon),
            typeof(string),
            typeof(NavSection),
            new PropertyMetadata("\uea3a")
        );
    
    private static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(NavSection),
            new PropertyMetadata(string.Empty)
        );
    
    private static readonly DependencyProperty IsExpandedProperty =
        DependencyProperty.Register(
            nameof(IsExpanded),
            typeof(bool),
            typeof(NavSection),
            new PropertyMetadata(true)
        );

    public NavSection()
    {
        InitializeComponent();
    }

    public string Icon
    {
        get => (string)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }
    
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public bool IsExpanded
    {
        get => (bool)GetValue(IsExpandedProperty);
        set => SetValue(IsExpandedProperty, value);
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        IsExpanded = !IsExpanded;
    }
}