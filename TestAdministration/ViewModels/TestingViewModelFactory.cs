using TestAdministration.Models.Data;
using TestAdministration.Models.Services;
using TestAdministration.Models.Storages;
using TestAdministration.Models.TestBuilders;
using TestAdministration.Models.Utils;

namespace TestAdministration.ViewModels;

/// <summary>
/// A factory for runtime creation of <see cref="TestingViewModel"/>
/// objects with specific storage and test types.
/// </summary>
public class TestingViewModelFactory(
    UserService userService,
    ITestBuilderFactory testBuilderFactory,
    IDateTimeProvider dateTimeProvider
)
{
    public TestingViewModel Create(ITestStorage testStorage, TestType testType) => new(
        userService,
        testBuilderFactory,
        testStorage,
        dateTimeProvider,
        testType
    );
}