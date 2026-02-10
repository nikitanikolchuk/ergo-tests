using System.Windows;
using TestAdministration.Models.Data;
using TestAdministration.Models.Storages;
using TestAdministration.ViewModels.Testing.Results;
using Wpf.Ui;

namespace TestAdministration.ViewModels.Testing;

/// <summary>
/// A factory for runtime creation of <see cref="TestingViewModel"/>
/// objects with specific test types.
/// </summary>
public class TestingViewModelFactory(
    IContentDialogService contentDialogService,
    ITestStorage testStorage,
    LayoutStateViewModel layoutStateViewModel,
    TestConductionViewModelFactory testConductionViewModelFactory,
    ResultsViewModelFactory resultsViewModelFactory
)
{
    public TestingViewModel Create(TestType testType, Action<bool> setIsNavPaneOpen, Action<Visibility> setHeaderVisibility) => new(
        contentDialogService,
        testStorage,
        layoutStateViewModel,
        testConductionViewModelFactory,
        resultsViewModelFactory,
        testType,
        setIsNavPaneOpen,
        setHeaderVisibility
    );
}