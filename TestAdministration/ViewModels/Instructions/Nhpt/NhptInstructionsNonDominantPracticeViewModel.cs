using TestAdministration.Models.Data;

namespace TestAdministration.ViewModels.Instructions.Nhpt;

public class NhptInstructionsNonDominantPracticeViewModel(
    Hand dominantHand
) : ViewModelBase
{
    public string TopText =>
        $"Po dokončení 3. pokusu testovací desku otočte tak, aby její část se zásobníkem byla na straně nedominantní" +
        $" ({NonDominantHandGenitive}) ruky probanda. Střed desky zarovnejte přibližně se středem trupu probanda." +
        $" Vzdálenost desky od hrany stolu si libovolně nastaví proband.";

    public string AudioInstruction =>
        $"„Nyní zopakujeme to samé s vaší {NonDominantHandInstrumental} rukou. Nejprve opět provedeme zkušební pokus." +
        $" Uchopte desku oběma rukama. Jste připraven/a?“";

    private string NonDominantHandGenitive => dominantHand == Hand.Right ? "levé" : "pravé";
    private string NonDominantHandInstrumental => dominantHand == Hand.Right ? "levou" : "pravou";
}