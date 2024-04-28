using TestAdministration.Models.Data;

namespace TestAdministration.ViewModels.Instructions.Bbt;

public class BbtInstructionsNonDominantPracticeViewModel(
    AudioInstructionResolver audioResolver,
    Hand dominantHand
) : ViewModelBase
{
    public string NonDominantHand => dominantHand == Hand.Right ? "levou" : "pravou";
    public ViewModelBase AudioInstructionViewModel => audioResolver.Get(0);
}