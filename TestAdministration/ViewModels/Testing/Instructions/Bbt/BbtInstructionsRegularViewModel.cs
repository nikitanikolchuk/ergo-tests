using TestAdministration.Models.Data;

namespace TestAdministration.ViewModels.Testing.Instructions.Bbt;

public class BbtInstructionsRegularViewModel(
    AudioInstructionResolver audioResolver,
    int section,
    int trial,
    int totalTrialCount,
    Hand dominantHand
) : ViewModelBase, IInstructionsPageViewModel
{
    public string AudioInstruction => _getAudioInstruction();

    public InstructionPlayerViewModel FirstAudioInstructionViewModel { get; } = audioResolver.Get(0);

    private string CurrentHand => section == 0 ? DominantHand : NonDominantHand;
    private string DominantHand => dominantHand == Hand.Right ? "pravou" : "levou";
    private string NonDominantHand => dominantHand == Hand.Right ? "levou" : "pravou";

    private string _getAudioInstruction()
    {
        if (trial == 1)
        {
            return "„Toto už bude skutečný test. Instrukce zůstávají stejné. Pracujte co nejrychleji." +
                   " Položte obě ruce po stranách krabice.“";
        }

        if (trial == totalTrialCount - 1)
        {
            return $"„Teď ještě naposledy zopakujeme to samé s vaší {CurrentHand} rukou." +
                   $" Instrukce zůstávají stejné. Pracujte co nejrychleji. Položte obě ruce po stranách krabice.“";
        }
        
        return $"„Teď ještě jednou zopakujeme to samé s vaší {CurrentHand} rukou. Instrukce zůstávají stejné." +
               $" Pracujte co nejrychleji. Položte obě ruce po stranách krabice.“";
    }
}