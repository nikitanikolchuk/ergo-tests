using TestAdministration.Models.Data;
using TestAdministration.Models.Services;
using TestAdministration.Models.Storages;
using TestAdministration.Models.Storages.Converters;
using TestAdministration.Models.TestBuilders;
using TestAdministration.Models.Utils;

namespace TestAdministration.ViewModels;

/// <summary>
/// A factory for runtime creation of <see cref="TestingViewModel"/>
/// objects with specific storage and test types.
/// </summary>
public class TestingViewModelFactory(
    AudioInstructionService audioInstructionService,
    UserService userService,
    ITestBuilderFactory testBuilderFactory,
    IDateTimeProvider dateTimeProvider,
    AgeCalculatorService ageCalculatorService,
    DocumentationConverter documentationConverter
)
{
    public TestingViewModel Create(ITestStorage testStorage, TestType testType) => new(
        audioInstructionService,
        userService,
        testBuilderFactory,
        testStorage,
        documentationConverter,
        dateTimeProvider,
        ageCalculatorService,
        testType
    );
}