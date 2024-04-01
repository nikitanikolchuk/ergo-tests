using Microsoft.Extensions.DependencyInjection;
using TestAdministration.ViewModels;

namespace TestAdministration;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    public static IServiceProvider ServiceProvider { get; private set; }

    static App()
    {
        var services = _configureServices();
        ServiceProvider = services.BuildServiceProvider();
    }

    private static IServiceCollection _configureServices() =>
        new ServiceCollection()
            .AddSingleton<MainViewModel>();
}