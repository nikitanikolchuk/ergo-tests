using TestAdministration.Models.Data;
using TestAdministration.Models.Storages;
using TestAdministration.ViewModels.Results;

namespace TestAdministration.ViewModels;

/// <summary>
/// A factory for runtime creation of <see cref="TestingViewModel"/>
/// objects with specific test types.
/// </summary>
public class TestingViewModelFactory(
    ITestStorage testStorage,
    TestConductionViewModelFactory testConductionViewModelFactory,
    ResultsViewModelFactory resultsViewModelFactory
)
{
    public TestingViewModel Create(TestType testType) => new(
        testStorage,
        testConductionViewModelFactory,
        resultsViewModelFactory,
        testType
    );
}