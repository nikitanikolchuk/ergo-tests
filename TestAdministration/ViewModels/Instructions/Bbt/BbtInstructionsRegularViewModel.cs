using TestAdministration.Models.Data;

namespace TestAdministration.ViewModels.Instructions.Bbt;

public class BbtInstructionsRegularViewModel(
    int section,
    int trial,
    Hand dominantHand
) : ViewModelBase
{
    public string AudioInstruction =>
        trial == 1
            ? "„Toto už bude skutečný test. Instrukce zůstávají stejné. Pracujte co nejrychleji. Položte obě ruce po" +
              " stranách krabice.“"
            : $"„Teď ještě jednou zopakujeme to samé s vaší {CurrentHand} rukou. Instrukce zůstávají stejné." +
              $" Pracujte co nejrychleji. Položte obě ruce po stranách krabice.“";

    private string CurrentHand => section == 0 ? DominantHand : NonDominantHand;
    private string DominantHand => dominantHand == Hand.Right ? "pravou" : "levou";
    private string NonDominantHand => dominantHand == Hand.Right ? "levou" : "pravou";
}