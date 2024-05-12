using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using Wpf.Ui.Input;
using MessageBox = Wpf.Ui.Controls.MessageBox;

namespace TestAdministration.ViewModels;

/// <summary>
/// A view model for displaying info about video manuals.
/// </summary>
public class VideoManualsViewModel : ViewModelBase
{
    public ICommand OnOpenLink => new RelayCommand<string>(_onOpenLink);
    public ICommand OnCopyEmail => new RelayCommand<string>(_onCopyEmail);

    private static void _onOpenLink(string? link)
    {
        if (link is null)
        {
            return;
        }

        Process.Start(new ProcessStartInfo(link) { UseShellExecute = true });
    }

    private static async void _onCopyEmail(string? email)
    {
        if (email is null)
        {
            return;
        }
        
        Clipboard.SetText(email);

        var messageBox = new MessageBox
        {
            Title = "Informace",
            Content = "E-mail byl zkopírován",
            CloseButtonText = "Zavřít"
        };

        await messageBox.ShowDialogAsync();
    }
}