using TestAdministration.Models.Data;
using TestAdministration.Models.Services;
using TestAdministration.Models.TestBuilders;

namespace TestAdministration.ViewModels.Instructions.Bbt;

public class BbtInstructionsViewModel(
    AudioInstructionService audioService,
    ITestBuilder testBuilder,
    Patient patient
) : ViewModelBase, IInstructionsViewModel
{
    public ViewModelBase CurrentViewModel => (testBuilder.CurrentSection, testBuilder.CurrentTrial) switch
    {
        (0, 0) => new BbtInstructionsDominantPracticeViewModel(
            _getAudioResolver(0, 0),
            patient.DominantHand
        ),
        (1, 0) => new BbtInstructionsNonDominantPracticeViewModel(
            _getAudioResolver(1, 0),
            patient.DominantHand
        ),
        _ => new BbtInstructionsRegularViewModel(
            _getAudioResolver(testBuilder.CurrentSection, testBuilder.CurrentTrial),
            testBuilder.CurrentSection,
            testBuilder.CurrentTrial,
            patient.DominantHand
        )
    };

    private AudioInstructionResolver _getAudioResolver(int section, int trial) => new(
        audioService,
        TestType.Bbt,
        patient,
        section,
        trial
    );
}