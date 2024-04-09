using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace TestAdministration.Views;

public partial class NavButton
{
    public event RoutedEventHandler? Click;

    private static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(
            nameof(Icon),
            typeof(string),
            typeof(NavButton),
            new PropertyMetadata("\uea3a")
        );

    private static readonly DependencyProperty OverlaySymbolProperty =
        DependencyProperty.Register(
            nameof(OverlaySymbol),
            typeof(string),
            typeof(NavButton),
            new PropertyMetadata(string.Empty)
        );

    private static readonly DependencyProperty OverlayIconProperty =
        DependencyProperty.Register(
            nameof(OverlayIcon),
            typeof(string),
            typeof(NavButton),
            new PropertyMetadata(string.Empty)
        );

    private static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(NavButton),
            new PropertyMetadata(string.Empty)
        );

    private static readonly DependencyProperty TextColorProperty =
        DependencyProperty.Register(
            nameof(TextColor),
            typeof(Brush),
            typeof(NavButton),
            new PropertyMetadata(new BrushConverter().ConvertFromString("#E5000000"))
        );

    private static readonly DependencyProperty IsExpandableProperty =
        DependencyProperty.Register(
            nameof(IsExpandable),
            typeof(bool),
            typeof(NavButton),
            new PropertyMetadata(false)
        );

    private static readonly DependencyProperty IsExpandedProperty =
        DependencyProperty.Register(
            nameof(IsExpanded),
            typeof(bool),
            typeof(NavButton),
            new PropertyMetadata(false)
        );

    private static readonly DependencyProperty IsIndentedProperty =
        DependencyProperty.Register(
            nameof(IsIndented),
            typeof(bool),
            typeof(NavButton),
            new PropertyMetadata(false)
        );

    private static readonly DependencyProperty IsSelectedProperty =
        DependencyProperty.Register(
            nameof(IsSelected),
            typeof(bool),
            typeof(NavButton),
            new PropertyMetadata(false)
        );

    private static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(
            nameof(Command),
            typeof(ICommand),
            typeof(NavButton),
            new PropertyMetadata(null)
        );

    public NavButton()
    {
        InitializeComponent();
    }

    public string Icon
    {
        get => (string)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public string OverlaySymbol
    {
        get => (string)GetValue(OverlaySymbolProperty);
        set => SetValue(OverlaySymbolProperty, value);
    }

    public string OverlayIcon
    {
        get => (string)GetValue(OverlayIconProperty);
        set => SetValue(OverlayIconProperty, value);
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public Brush TextColor
    {
        get => (Brush)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public bool IsExpandable
    {
        get => (bool)GetValue(IsExpandableProperty);
        set => SetValue(IsExpandableProperty, value);
    }

    public bool IsExpanded
    {
        get => (bool)GetValue(IsExpandedProperty);
        set => SetValue(IsExpandedProperty, value);
    }

    public bool IsIndented
    {
        get => (bool)GetValue(IsIndentedProperty);
        set => SetValue(IsIndentedProperty, value);
    }

    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }

    public ICommand? Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    private void OnClick(object sender, RoutedEventArgs e) => Click?.Invoke(this, e);
}