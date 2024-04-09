using Microsoft.Extensions.DependencyInjection;

namespace TestAdministration.ViewModels;

/// <summary>
/// A class implementing service locator pattern for ViewModels.
/// </summary>
public class ViewModelLocator
{
    public static MainViewModel MainViewModel => _getViewModel<MainViewModel>();
    public static LoginScreenViewModel LoginScreenViewModel => _getViewModel<LoginScreenViewModel>();
    public static MainScreenViewModel MainScreenViewModel => _getViewModel<MainScreenViewModel>();
    public static NavBarViewModel NavBarViewModel => _getViewModel<NavBarViewModel>();

    private static T _getViewModel<T>() =>
        App.ServiceProvider.GetService<T>()
        ?? throw new InvalidOperationException($"Missing {typeof(T)} service");
}