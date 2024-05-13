using TestAdministration.Models.Data;

namespace TestAdministration.ViewModels.Instructions.Bbt;

public class BbtInstructionsRegularViewModel(
    AudioInstructionResolver audioResolver,
    int section,
    int trial,
    Hand dominantHand
) : ViewModelBase, IInstructionsPageViewModel
{
    public string AudioInstruction => trial switch
    {
        1 => "„Toto už bude skutečný test. Instrukce zůstávají stejné. Pracujte co nejrychleji. Položte obě ruce po" +
             " stranách krabice.“",
        2 => $"„Teď ještě jednou zopakujeme to samé s vaší {CurrentHand} rukou. Instrukce zůstávají stejné." +
             $" Pracujte co nejrychleji. Položte obě ruce po stranách krabice.“",
        _ => $"„Teď ještě naposledy zopakujeme to samé s vaší {CurrentHand} rukou. Instrukce zůstávají stejné." +
             $" Pracujte co nejrychleji. Položte obě ruce po stranách krabice.“"
    };

    public InstructionPlayerViewModel FirstAudioInstructionViewModel { get; } = audioResolver.Get(0);

    private string CurrentHand => section == 0 ? DominantHand : NonDominantHand;
    private string DominantHand => dominantHand == Hand.Right ? "pravou" : "levou";
    private string NonDominantHand => dominantHand == Hand.Right ? "levou" : "pravou";
}