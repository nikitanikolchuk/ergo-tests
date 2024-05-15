using TestAdministration.Models.Data;
using TestAdministration.Models.Services;
using TestAdministration.Models.TestBuilders;
using TestAdministration.Models.Utils;
using Wpf.Ui;

namespace TestAdministration.ViewModels.Testing;

public class TestConductionViewModelFactory(
    IContentDialogService contentDialogService,
    ConfigurationService configurationService,
    AudioInstructionService audioInstructionService,
    VideoRecorderService videoRecorderService,
    ITestBuilderFactory testBuilderFactory,
    IDateTimeProvider dateTimeProvider
)
{
    public Testing.TestConductionViewModel Create(
        Patient patient,
        TestType testType,
        Action<Patient, Test> onShowResults
    ) => new(
        contentDialogService,
        audioInstructionService,
        videoRecorderService,
        testBuilderFactory,
        dateTimeProvider,
        configurationService.CurrentUser,
        patient,
        testType,
        onShowResults
    );
}