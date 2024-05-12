using System.Diagnostics;
using System.Windows.Input;
using Wpf.Ui.Input;

namespace TestAdministration.ViewModels;

/// <summary>
/// A view model for displaying info about test results'
/// directory.
/// </summary>
public class ResultsStorageViewModel(
    string directoryPath
) : ViewModelBase
{
    public ICommand OnOpenResultsDirectory => new RelayCommand<object?>(_ => _onOpenResultsDirectory());

    private void _onOpenResultsDirectory()
    {
        Process.Start("explorer.exe", directoryPath);
    }
}