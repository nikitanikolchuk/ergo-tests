using System.Diagnostics;
using System.Windows.Input;
using Wpf.Ui.Input;

namespace TestAdministration.ViewModels;

/// <summary>
/// A view model for displaying info about text manuals.
/// </summary>
public class TextManualsViewModel : ViewModelBase
{
    public ICommand OnOpenLink => new RelayCommand<string>(_onOpenLink);

    private static void _onOpenLink(string? link)
    {
        if (link is null)
        {
            return;
        }

        Process.Start(new ProcessStartInfo(link) { UseShellExecute = true });
    }
}