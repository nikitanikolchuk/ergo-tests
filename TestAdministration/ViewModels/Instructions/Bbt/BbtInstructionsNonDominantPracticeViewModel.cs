using TestAdministration.Models.Data;

namespace TestAdministration.ViewModels.Instructions.Bbt;

public class BbtInstructionsNonDominantPracticeViewModel(
    Hand dominantHand
) : ViewModelBase
{
    public string NonDominantHand => dominantHand == Hand.Right ? "levou" : "pravou";
}