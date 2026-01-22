namespace TestAdministration.ViewModels;

/// <summary>
/// A view model that should be included any other view model that changes dynamically
/// based on the layout mode.
/// </summary>
public class LayoutStateViewModel : ViewModelBase
{
    public event Action<bool>? OnLayoutUpdated;
    
    public bool IsCompactLayout
    {
        get;
        set
        {
            field = value;
            OnLayoutUpdated?.Invoke(field);
            OnPropertyChanged();
        }
    } = false;
}