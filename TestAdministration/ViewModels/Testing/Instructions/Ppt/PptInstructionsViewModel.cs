using TestAdministration.Models.Data;
using TestAdministration.Models.Services;
using TestAdministration.Models.TestBuilders;

namespace TestAdministration.ViewModels.Testing.Instructions.Ppt;

public class PptInstructionsViewModel(
    AudioInstructionService audioService,
    ITestBuilder testBuilder,
    Patient patient
) : ViewModelBase, IInstructionsViewModel
{
    public ViewModelBase CurrentViewModel
    {
        get
        {
            var viewModel = _getViewModel();
            var firstAudioPlayerViewModel = viewModel.FirstAudioInstructionViewModel;
            audioService.AudioPlayer = firstAudioPlayerViewModel.AudioPlayer;

            return (ViewModelBase)viewModel;
        }
    }

    private IInstructionsPageViewModel _getViewModel() => (testBuilder.CurrentSection, testBuilder.CurrentTrial) switch
    {
        (0, 0) => new PptInstructionsDominantHandFirstViewModel(
            _getAudioResolver(0, 0),
            patient.DominantHand,
            testBuilder.TotalTrialCount
        ),
        (1, 0) => new PptInstructionsNonDominantHandFirstViewModel(
            _getAudioResolver(1, 0),
            patient.DominantHand,
            testBuilder.TotalTrialCount
        ),
        (0, _) => new PptInstructionsSingleHandRegularViewModel(
            _getAudioResolver(0, testBuilder.CurrentTrial),
            testBuilder.CurrentSection,
            testBuilder.CurrentTrial,
            patient.DominantHand,
            testBuilder.TotalTrialCount
        ),
        (1, _) => new PptInstructionsSingleHandRegularViewModel(
            _getAudioResolver(1, testBuilder.CurrentTrial),
            testBuilder.CurrentSection,
            testBuilder.CurrentTrial,
            patient.DominantHand,
            testBuilder.TotalTrialCount
        ),
        (2, 0) => new PptInstructionsBothHandsFirstViewModel(
            _getAudioResolver(2, 0),
            patient.DominantHand,
            testBuilder.TotalTrialCount
        ),
        (2, _) => new PptInstructionsBothHandsRegularViewModel(
            _getAudioResolver(2, testBuilder.CurrentTrial),
            testBuilder.CurrentTrial,
            testBuilder.TotalTrialCount
        ),
        (3, 0) => new PptInstructionsAssemblyFirstViewModel(
            _getAudioResolver(3, 0),
            patient.DominantHand,
            testBuilder.TotalTrialCount
        ),
        _ => new PptInstructionsAssemblyRegularViewModel(
            _getAudioResolver(testBuilder.CurrentSection, testBuilder.CurrentTrial),
            testBuilder.CurrentTrial,
            testBuilder.TotalTrialCount
        )
    };

    private AudioInstructionResolver _getAudioResolver(int section, int trial)
    {
        // If trial is 2/2, choose the last audio
        // "will repeat one last time" instead of "will repeat once more"
        if (trial == 1 && testBuilder.TotalTrialCount == 2)
        {
            trial = 2;
        }

        return new AudioInstructionResolver(
            audioService,
            TestType.Ppt,
            patient,
            section,
            trial
        );
    }
}