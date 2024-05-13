using TestAdministration.Models.Data;
using TestAdministration.Models.Services;
using TestAdministration.Models.TestBuilders;

namespace TestAdministration.ViewModels.Instructions.Ppt;

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
            var firstAudioPlayer = viewModel.FirstAudioInstructionViewModel;
            audioService.SetPlayerActions(
                firstAudioPlayer.OnResume,
                firstAudioPlayer.OnPause,
                firstAudioPlayer.OnStop
            );

            return (ViewModelBase)viewModel;
        }
    }

    private IInstructionsPageViewModel _getViewModel() => (testBuilder.CurrentSection, testBuilder.CurrentTrial) switch
    {
        (0, 0) => new PptInstructionsDominantHandFirstViewModel(
            _getAudioResolver(0, 0),
            patient.DominantHand
        ),
        (1, 0) => new PptInstructionsNonDominantHandFirstViewModel(
            _getAudioResolver(1, 0),
            patient.DominantHand
        ),
        (0, _) => new PptInstructionsSingleHandRegularViewModel(
            _getAudioResolver(0, testBuilder.CurrentTrial),
            testBuilder.CurrentSection,
            testBuilder.CurrentTrial,
            patient.DominantHand
        ),
        (1, _) => new PptInstructionsSingleHandRegularViewModel(
            _getAudioResolver(1, testBuilder.CurrentTrial),
            testBuilder.CurrentSection,
            testBuilder.CurrentTrial,
            patient.DominantHand
        ),
        (2, 0) => new PptInstructionsBothHandsFirstViewModel(
            _getAudioResolver(2, 0),
            patient.DominantHand
        ),
        (2, _) => new PptInstructionsBothHandsRegularViewModel(
            _getAudioResolver(2, testBuilder.CurrentTrial),
            testBuilder.CurrentTrial
        ),
        (3, 0) => new PptInstructionsAssemblyFirstViewModel(
            _getAudioResolver(3, 0),
            patient.DominantHand
        ),
        _ => new PptInstructionsAssemblyRegularViewModel(
            _getAudioResolver(testBuilder.CurrentSection, testBuilder.CurrentTrial),
            testBuilder.CurrentTrial
        )
    };

    private AudioInstructionResolver _getAudioResolver(int section, int trial) => new(
        audioService,
        TestType.Ppt,
        patient,
        section,
        trial
    );
}