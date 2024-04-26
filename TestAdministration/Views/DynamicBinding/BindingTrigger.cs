using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace TestAdministration.Views.DynamicBinding;

/// <summary>
/// A helper class for manual forcing binding refreshes.
/// </summary>
public class BindingTrigger : INotifyPropertyChanged
{
    public BindingTrigger() =>
        Binding = new Binding
        {
            Source = this,
            Path = new PropertyPath(nameof(Value))
        };

    public event PropertyChangedEventHandler? PropertyChanged;

    public Binding Binding { get; }
    public object? Value { get; set; }

    public void Refresh() =>
        PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs(nameof(Value))
        );
}