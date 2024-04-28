using TestAdministration.Models.Data;

namespace TestAdministration.ViewModels.Instructions.Bbt;

public class BbtInstructionsDominantPracticeViewModel(
    AudioInstructionResolver audioResolver,
    Hand dominantHand
) : ViewModelBase
{
    public string DominantHand => dominantHand == Hand.Right ? "pravou" : "levou";
    public ViewModelBase FirstAudioInstructionViewModel => audioResolver.Get(0, true);
    public ViewModelBase SecondAudioInstructionViewModel => audioResolver.Get(1, true);
}