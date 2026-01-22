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
    IDateTimeProvider dateTimeProvider,
    LayoutStateViewModel layoutStateViewModel
)
{
    public TestConductionViewModel Create(
        Patient patient,
        int trialCount,
        TestType testType,
        Action<Patient, Test> onShowResults
    ) => new(
        contentDialogService,
        audioInstructionService,
        videoRecorderService,
        testBuilderFactory,
        dateTimeProvider,
        layoutStateViewModel,
        configurationService.CurrentUser,
        patient,
        trialCount,
        testType,
        onShowResults
    );
}