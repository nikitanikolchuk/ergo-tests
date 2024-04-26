using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Wpf.Ui.Controls;
using TextBlock = Wpf.Ui.Controls.TextBlock;

namespace TestAdministration.Views;

/// <summary>
/// Custom extension of <see cref="FontIcon"/> that puts a circle
/// behind the glyph.
/// </summary>
public class CircleFontIcon : FontIcon
{
    protected override UIElement InitializeChildren()
    {
        if (VisualParent is not null)
        {
            SetCurrentValue(FontSizeProperty, TextElement.GetFontSize(VisualParent));
        }

        if (FontSize.Equals(SystemFonts.MessageFontSize))
        {
            SetResourceReference(FontSizeProperty, "DefaultIconFontSize");
        }

        var grid = new Grid();

        var circleIcon = new SymbolIcon
        {
            Symbol = SymbolRegular.Circle24,
            FontSize = FontSize
        };

        TextBlock = new TextBlock
        {
            Style = null,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Center,
            TextAlignment = TextAlignment.Center,
            FontFamily = new FontFamily("Segoe UI Variable"),
            FontSize = FontSize - 8,
            FontStyle = FontStyle,
            FontWeight = FontWeights.SemiBold,
            Text = Glyph,
            Visibility = Visibility.Visible,
            Focusable = false,
        };

        grid.Children.Add(circleIcon);
        grid.Children.Add(TextBlock);

        SetCurrentValue(FocusableProperty, false);

        return grid;
    }
}