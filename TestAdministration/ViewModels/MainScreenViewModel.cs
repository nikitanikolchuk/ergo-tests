namespace TestAdministration.ViewModels;

public class MainScreenViewModel(
    InitContentViewModel initContentViewModel
) : ViewModelBase
{
    private ViewModelBase _contentViewModel = initContentViewModel;

    /// <summary>
    /// View model with an associated view that fills the content
    /// part of the screen.
    /// </summary>
    public ViewModelBase ContentViewModel
    {
        get => _contentViewModel;
        private set
        {
            if (_contentViewModel == value)
            {
                return;
            }

            _contentViewModel = value;
            OnPropertyChanged();
        }
    }
}