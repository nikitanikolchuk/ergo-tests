using TestAdministration.Models.Data;

namespace TestAdministration.ViewModels.Testing.Instructions.Nhpt;

public class NhptInstructionsRegularViewModel(
    AudioInstructionResolver audioResolver,
    int trial,
    Hand dominantHand
) : ViewModelBase, IInstructionsPageViewModel
{
    public string TopText =>
        trial == 1
            ? "Poté, co proband dokončí zkušební pokus, řekněte/přehrajte nahrávku:"
            : "Dále řekněte/přehrajte nahrávku:";

    public string AudioInstruction => trial switch
    {
        1 => "„Toto už bude skutečný test. Instrukce zůstávají stejné. Pracujte co nejrychleji. Uchopte desku oběma" +
             " rukama. Jste připraven/a?“",
        2 => $"„Nyní ještě jednou zopakujeme to samé s vaší {DominantHand} rukou. Instrukce zůstávají stejné." +
             $" Pracujte co nejrychleji. Uchopte desku oběma rukama. Jste připraven/a?“",
        _ => $"„Nyní ještě naposledy zopakujeme to samé s vaší {DominantHand} rukou. Instrukce zůstávají stejné." +
             $" Pracujte co nejrychleji. Uchopte desku oběma rukama. Jste připraven/a?“"
    };

    public InstructionPlayerViewModel FirstAudioInstructionViewModel { get; } =
        audioResolver.Get(0, true);

    private string DominantHand => dominantHand == Hand.Right ? "pravou" : "levou";
}