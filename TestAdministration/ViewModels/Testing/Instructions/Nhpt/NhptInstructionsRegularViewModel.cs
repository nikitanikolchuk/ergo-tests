using TestAdministration.Models.Data;

namespace TestAdministration.ViewModels.Testing.Instructions.Nhpt;

public class NhptInstructionsRegularViewModel(
    AudioInstructionResolver audioResolver,
    int trial,
    Hand dominantHand,
    int totalTrialCount
) : ViewModelBase, IInstructionsPageViewModel
{
    public string TopText =>
        trial == 1
            ? "Poté, co proband dokončí zkušební pokus, řekněte/přehrajte nahrávku:"
            : "Dále řekněte/přehrajte nahrávku:";

    public string AudioInstruction => _getAudioInstruction();

    public InstructionPlayerViewModel FirstAudioInstructionViewModel { get; } =
        audioResolver.Get(0, true);

    private string DominantHand => dominantHand == Hand.Right ? "pravou" : "levou";

    private string _getAudioInstruction()
    {
        if (trial == 1)
        {
            return "„Toto už bude skutečný test. Instrukce zůstávají stejné. Pracujte co nejrychleji." +
                   " Uchopte desku oběma rukama. Jste připraven/a?“";
        }

        if (trial == totalTrialCount - 1)
        {
            return $"„Nyní ještě naposledy zopakujeme to samé s vaší {DominantHand} rukou." +
                   $" Instrukce zůstávají stejné. Pracujte co nejrychleji. Uchopte desku oběma rukama." +
                   $" Jste připraven/a?“";
        }

        return $"„Nyní ještě jednou zopakujeme to samé s vaší {DominantHand} rukou. Instrukce zůstávají stejné." +
               $" Pracujte co nejrychleji. Uchopte desku oběma rukama. Jste připraven/a?“";
    }
}