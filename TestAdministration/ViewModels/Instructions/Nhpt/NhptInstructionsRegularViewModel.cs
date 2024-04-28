using TestAdministration.Models.Data;

namespace TestAdministration.ViewModels.Instructions.Nhpt;

public class NhptInstructionsRegularViewModel(
    int trial,
    Hand dominantHand
) : ViewModelBase
{
    public string TopText =>
        trial == 1
            ? "Poté, co proband dokončí zkušební pokus, řekněte/přehrajte nahrávku:"
            : "Dále řekněte/přehrajte nahrávku:";

    public string AudioInstruction =>
        trial == 1
            ? "„Toto už bude skutečný test. Instrukce zůstávají stejné. Pracujte co nejrychleji. Uchopte desku oběma" +
              " rukama. Jste připraven/a?“"
            : $"„Nyní ještě jednou zopakujeme to samé s vaší {DominantHand} rukou. Instrukce zůstávají stejné." +
              $" Pracujte co nejrychleji. Uchopte desku oběma rukama. Jste připraven/a?“";

    private string DominantHand => dominantHand == Hand.Right ? "pravou" : "levou";
}