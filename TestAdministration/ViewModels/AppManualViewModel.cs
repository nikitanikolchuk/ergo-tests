namespace TestAdministration.ViewModels;

/// <summary>
/// A view model for binding a page with info about using the app. 
/// </summary>
public class AppManualViewModel(LayoutStateViewModel layoutState) : ViewModelBase
{
    public LayoutStateViewModel LayoutState { get; set; } = layoutState;
}