using TestAdministration.Models.Data;

namespace TestAdministration.ViewModels.Instructions.Ppt;

public class PptInstructionsSingleHandRegularViewModel(
    int trial,
    Hand dominantHand
) : ViewModelBase
{
    public string TopOralInstruction =>
        trial == 2
            ? $"„Teď ještě jednou zopakujeme to samé s vaší {DominantHand} rukou. Instrukce zůstávají stejné." +
              $" Pracujte co nejrychleji. Položte obě ruce po stranách desky. Jste připraven/a?“"
            : $"„Teď ještě naposledy zopakujeme to samé s vaší {DominantHand} rukou. Instrukce zůstávají stejné." +
              $" Pracujte co nejrychleji. Položte obě ruce po stranách desky. Jste připraven/a?“";

    public string BottomOralInstruction =>
        $"„Děkuji. Nyní, prosím, vraťte kolíky zpět do zásobníku {ReturnDirection}.“";

    private string DominantHand => dominantHand == Hand.Right ? "pravou" : "levou";
    private string ReturnDirection => dominantHand == Hand.Right ? "vpravo" : "vlevo";
}