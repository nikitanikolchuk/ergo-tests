using TestAdministration.Models.Data;

namespace TestAdministration.ViewModels.Instructions.Bbt;

public class BbtInstructionsDominantPracticeViewModel : ViewModelBase, IInstructionsPageViewModel
{
    public BbtInstructionsDominantPracticeViewModel(AudioInstructionResolver audioResolver, Hand dominantHand)
    {
        DominantHand = dominantHand == Hand.Right ? "pravou" : "levou";
        SecondAudioInstructionViewModel = audioResolver.Get(1, true);
        FirstAudioInstructionViewModel = audioResolver.Get(0, true, SecondAudioInstructionViewModel);
    }

    public string DominantHand { get; }
    public InstructionPlayerViewModel FirstAudioInstructionViewModel { get; }
    public InstructionPlayerViewModel SecondAudioInstructionViewModel { get; }
}