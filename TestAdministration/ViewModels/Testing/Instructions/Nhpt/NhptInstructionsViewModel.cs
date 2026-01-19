using TestAdministration.Models.Data;
using TestAdministration.Models.Services;
using TestAdministration.Models.TestBuilders;

namespace TestAdministration.ViewModels.Testing.Instructions.Nhpt;

public class NhptInstructionsViewModel(
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
        (0, 0) => new NhptInstructionsDominantPracticeViewModel(
            _getAudioResolver(0, 0),
            patient.DominantHand,
            testBuilder.TotalTrialCount
        ),
        (1, 0) => new NhptInstructionsNonDominantPracticeViewModel(
            _getAudioResolver(1, 0),
            patient.DominantHand,
            testBuilder.TotalTrialCount
        ),
        _ => new NhptInstructionsRegularViewModel(
            _getAudioResolver(testBuilder.CurrentSection, testBuilder.CurrentTrial),
            testBuilder.CurrentTrial,
            patient.DominantHand,
            testBuilder.TotalTrialCount
        ),
    };

    private AudioInstructionResolver _getAudioResolver(int section, int trial)
    {
        // If trial is 2/2 (excluding practice trial), choose the last audio
        // "will repeat one last time" instead of "will repeat once more"
        if (trial == 2 && testBuilder.TotalTrialCount == 3)
        {
            trial = 3;
        }

        return new AudioInstructionResolver(
            audioService,
            TestType.Nhpt,
            patient,
            section,
            trial
        );
    }
}