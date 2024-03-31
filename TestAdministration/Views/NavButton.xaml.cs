using System.Windows;
using System.Windows.Input;

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

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public ICommand? Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    private void OnClick(object sender, RoutedEventArgs e) => Click?.Invoke(this, e);
}