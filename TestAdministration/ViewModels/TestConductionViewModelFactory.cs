using TestAdministration.Models.Data;
using TestAdministration.Models.Services;
using TestAdministration.Models.TestBuilders;
using TestAdministration.Models.Utils;

namespace TestAdministration.ViewModels;

public class TestConductionViewModelFactory(
    ConfigurationService configurationService,
    AudioInstructionService audioInstructionService,
    ITestBuilderFactory testBuilderFactory,
    IDateTimeProvider dateTimeProvider
)
{
    public TestConductionViewModel Create(
        Patient patient,
        TestType testType,
        Action<Patient, Test> onShowResults
    ) => new(
        audioInstructionService,
        testBuilderFactory,
        dateTimeProvider,
        configurationService.CurrentUser,
        patient,
        testType,
        onShowResults
    );
}