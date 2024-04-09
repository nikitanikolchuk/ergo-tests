using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TestAdministration.ViewModels;

/// <summary>
/// A basic implementation of <c>INotifyPropertyChanged</c>.
/// </summary>
public abstract class ViewModelBase: INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}