using System.Windows;
using CsvHelper.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestAdministration.Models.Data;
using TestAdministration.Models.Services;
using TestAdministration.Models.Storages;
using TestAdministration.Models.Storages.Exporters;
using TestAdministration.Models.Storages.FileSystems;
using TestAdministration.Models.Storages.Importers;
using TestAdministration.Models.Storages.Mappers;
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
        var mainWindowViewModel = serviceProvider.GetService<MainWindowViewModel>();
        if (mainWindowViewModel is null)
        {
            throw new InvalidOperationException($"Missing {typeof(MainWindowViewModel)} service");
        }

        var mainWindow = new MainWindow(mainWindowViewModel);

        MainWindow = mainWindow;
        MainWindow.Show();
    }

    private static IServiceCollection _configureServices() =>
        new ServiceCollection()
            .AddSingleton<MainWindowViewModel>()
            .AddSingleton<LoginScreenViewModel>()
            .AddSingleton<ConfigurationService>()
            .AddSingleton<UserService>()
            .AddSingleton<IContentDialogService, ContentDialogService>()
            .AddSingleton<MainScreenViewModelFactory>()
            .AddSingleton<TestStorageFactory>()
            .AddSingleton<LocalFileSystem>()
            .AddSingleton<LocalCsvImporter>()
            .AddSingleton<LocalCsvExporter>()
            .AddSingleton<ClassMap<Patient>, PatientCsvMapper>()
            .AddSingleton<NhptCsvMapper>()
            .AddSingleton<InitContentViewModel>()
            .AddSingleton<TestingViewModelFactory>();
}