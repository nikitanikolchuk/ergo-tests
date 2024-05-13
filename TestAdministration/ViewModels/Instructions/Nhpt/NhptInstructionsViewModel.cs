using TestAdministration.Models.Data;
using TestAdministration.Models.Services;
using TestAdministration.Models.TestBuilders;

namespace TestAdministration.ViewModels.Instructions.Nhpt;

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
        (0, 0) => new NhptInstructionsDominantPracticeViewModel(
            _getAudioResolver(0, 0),
            patient.DominantHand
        ),
        (1, 0) => new NhptInstructionsNonDominantPracticeViewModel(
            _getAudioResolver(1, 0),
            patient.DominantHand
        ),
        _ => new NhptInstructionsRegularViewModel(
            _getAudioResolver(testBuilder.CurrentSection, testBuilder.CurrentTrial),
            testBuilder.CurrentTrial,
            patient.DominantHand
        ),
    };

    private AudioInstructionResolver _getAudioResolver(int section, int trial) => new(
        audioService,
        TestType.Nhpt,
        patient,
        section,
        trial
    );
}