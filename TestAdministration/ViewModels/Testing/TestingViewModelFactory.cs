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
    TestConductionViewModelFactory testConductionViewModelFactory,
    ResultsViewModelFactory resultsViewModelFactory
)
{
    public TestingViewModel Create(TestType testType) => new(
        contentDialogService,
        testStorage,
        testConductionViewModelFactory,
        resultsViewModelFactory,
        testType
    );
}