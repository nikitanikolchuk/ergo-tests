using TestAdministration.Models.Data;

namespace TestAdministration.ViewModels.Instructions.Bbt;

public class BbtInstructionsNonDominantPracticeViewModel(
    AudioInstructionResolver audioResolver,
    Hand dominantHand
) : ViewModelBase, IInstructionsPageViewModel
{
    public string NonDominantHand { get; } = dominantHand == Hand.Right ? "levou" : "pravou";
    public InstructionPlayerViewModel FirstAudioInstructionViewModel { get; } = audioResolver.Get(0);
}