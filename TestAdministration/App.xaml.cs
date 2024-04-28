using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using TestAdministration.Models.Services;
using TestAdministration.Models.Storages;
using TestAdministration.Models.Storages.Converters;
using TestAdministration.Models.Storages.Exporters;
using TestAdministration.Models.Storages.FileSystems;
using TestAdministration.Models.Storages.Importers;
using TestAdministration.Models.TestBuilders;
using TestAdministration.Models.TestBuilders.SectionBuilders;
using TestAdministration.Models.TestBuilders.SectionBuilders.Calculators;
using TestAdministration.Models.Utils;
using TestAdministration.ViewModels;
using TestAdministration.Views;
using Wpf.Ui;
using Wpf.Ui.Appearance;

namespace TestAdministration;

public partial class App
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var serviceProvider = _configureServices().BuildServiceProvider();

        var configurationService = serviceProvider.GetService<ConfigurationService>();
        if (configurationService is null)
        {
            throw new InvalidOperationException($"Missing {typeof(ConfigurationService)} service");
        }

        ApplicationThemeManager.Apply(configurationService.ApplicationTheme);

        var fontSize = configurationService.FontSize;
        Current.Resources["BaseFontSize"] = (double)fontSize;
        Current.Resources["ControlContentThemeFontSize"] = (double)fontSize;

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
            .AddSingleton<PatientCsvConverter>()
            .AddSingleton<InitContentViewModel>()
            .AddSingleton<SettingsViewModel>()
            .AddSingleton<TestingViewModelFactory>()
            .AddSingleton<ITestBuilderFactory, TestBuilderFactory>()
            .AddSingleton<NhptTestSectionBuilder>()
            .AddSingleton<PptTestSectionBuilder>()
            .AddSingleton<BbtTestSectionBuilder>()
            .AddSingleton<ITestCalculator<NhptTestNormProvider>, TestCalculator<NhptTestNormProvider>>()
            .AddSingleton<ITestCalculator<PptTestNormProvider>, TestCalculator<PptTestNormProvider>>()
            .AddSingleton<ITestCalculator<BbtTestNormProvider>, TestCalculator<BbtTestNormProvider>>()
            .AddSingleton<IDateTimeProvider, DateTimeProvider>()
            .AddSingleton<NhptTestNormProvider>()
            .AddSingleton<PptTestNormProvider>()
            .AddSingleton<BbtTestNormProvider>()
            .AddSingleton<NhptCsvConverter>()
            .AddSingleton<PptCsvConverter>()
            .AddSingleton<BbtCsvConverter>()
            .AddSingleton<AudioInstructionService>();
}