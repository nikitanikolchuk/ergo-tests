using TestAdministration.Models.Data;

namespace TestAdministration.ViewModels.Instructions.Nhpt;

public class NhptInstructionsNonDominantPracticeViewModel(
    Hand dominantHand
) : ViewModelBase
{
    public string NonDominantHandGenitive => dominantHand == Hand.Right ? "levé" : "pravé";

    public string AudioInstruction =>
        $"„Nyní zopakujeme to samé s vaší {NonDominantHandInstrumental} rukou. Nejprve opět provedeme zkušební pokus." +
        $" Uchopte desku oběma rukama. Jste připraven/a?“";

    private string NonDominantHandInstrumental => dominantHand == Hand.Right ? "levou" : "pravou";
}