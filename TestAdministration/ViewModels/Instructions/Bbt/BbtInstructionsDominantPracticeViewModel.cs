using TestAdministration.Models.Data;

namespace TestAdministration.ViewModels.Instructions.Bbt;

public class BbtInstructionsDominantPracticeViewModel(
    Hand dominantHand
) : ViewModelBase
{
    public string DominantHand => dominantHand == Hand.Right ? "pravou" : "levou";
}