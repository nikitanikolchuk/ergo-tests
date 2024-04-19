using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using TestAdministration.Models.Services;
using TestAdministration.ViewModels;
using TestAdministration.Views;
using Wpf.Ui;

namespace TestAdministration;

public partial class App
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var serviceProvider = _configureServices().BuildServiceProvider();
        var mainWindowViewModel = serviceProvider.GetService<MainWindowViewModel>()
                                  ?? throw new InvalidOperationException(
                                      $"Missing {typeof(MainWindowViewModel)} service");
        var mainWindow = new MainWindow(mainWindowViewModel);

        MainWindow = mainWindow;
        MainWindow.Show();
    }

    private static IServiceCollection _configureServices() =>
        new ServiceCollection()
            .AddSingleton<ConfigurationService>()
            .AddSingleton<UserService>()
            .AddSingleton<IContentDialogService, ContentDialogService>()
            .AddSingleton<MainWindowViewModel>()
            .AddSingleton<LoginScreenViewModel>()
            .AddSingleton<MainScreenViewModel>()
            .AddSingleton<InitContentViewModel>();
}