using TestAdministration.Models.Data;
using TestAdministration.Models.Storages;

namespace TestAdministration.ViewModels;

/// <summary>
/// A factory for runtime creation of <see cref="TestingViewModel"/>
/// objects with specific storage and test types.
/// </summary>
public class TestingViewModelFactory
{
    public TestingViewModel Create(ITestStorage testStorage, TestType testType) => new(testStorage, testType);
}