using TestAdministration.Models.Data;

namespace TestAdministration.ViewModels.Instructions.Ppt;

public class PptInstructionsSingleHandRegularViewModel(
    AudioInstructionResolver audioResolver,
    int section,
    int trial,
    Hand dominantHand
) : ViewModelBase
{
    public string TopAudioInstruction =>
        trial == 1
            ? $"„Teď ještě jednou zopakujeme to samé s vaší {CurrentHand} rukou. Instrukce zůstávají stejné." +
              $" Pracujte co nejrychleji. Položte obě ruce po stranách desky. Jste připraven/a?“"
            : $"„Teď ještě naposledy zopakujeme to samé s vaší {CurrentHand} rukou. Instrukce zůstávají stejné." +
              $" Pracujte co nejrychleji. Položte obě ruce po stranách desky. Jste připraven/a?“";

    public string BottomAudioInstruction =>
        $"„Děkuji. Nyní, prosím, vraťte kolíky zpět do zásobníku {ReturnDirection}.“";

    public ViewModelBase FirstAudioInstructionViewModel => audioResolver.Get(0, true);
    public ViewModelBase SecondAudioInstructionViewModel => audioResolver.Get(1);

    private string CurrentHand => section == 0 ? DominantHand : NonDominantHand;
    private string DominantHand => dominantHand == Hand.Right ? "pravou" : "levou";
    private string NonDominantHand => dominantHand == Hand.Right ? "levou" : "pravou";
    private string ReturnDirection => dominantHand == Hand.Right ? "vpravo" : "vlevo";
}