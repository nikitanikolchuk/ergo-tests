using Microsoft.Extensions.DependencyInjection;

namespace TestAdministration.ViewModels;

/// <summary>
/// A class implementing service locator pattern for ViewModels.
/// </summary>
public class ViewModelLocator
{
    public static MainViewModel MainViewModel => _getViewModel<MainViewModel>();

    private static T _getViewModel<T>() =>
        App.ServiceProvider.GetService<T>()
        ?? throw new InvalidOperationException($"Missing {typeof(T)} service");
}