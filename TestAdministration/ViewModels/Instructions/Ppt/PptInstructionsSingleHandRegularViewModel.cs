using TestAdministration.Models.Data;

namespace TestAdministration.ViewModels.Instructions.Ppt;

public class PptInstructionsSingleHandRegularViewModel : ViewModelBase, IInstructionsPageViewModel
{
    public PptInstructionsSingleHandRegularViewModel(
        AudioInstructionResolver audioResolver,
        int section,
        int trial,
        Hand dominantHand
    )
    {
        DominantHand = dominantHand == Hand.Right ? "pravou" : "levou";
        NonDominantHand = dominantHand == Hand.Right ? "levou" : "pravou";
        CurrentHand = section == 0 ? DominantHand : NonDominantHand;
        ReturnDirection = dominantHand == Hand.Right ? "vpravo" : "vlevo";
        TopAudioInstruction =
            trial == 1
                ? $"„Teď ještě jednou zopakujeme to samé s vaší {CurrentHand} rukou. Instrukce zůstávají stejné." +
                  $" Pracujte co nejrychleji. Položte obě ruce po stranách desky. Jste připraven/a?“"
                : $"„Teď ještě naposledy zopakujeme to samé s vaší {CurrentHand} rukou. Instrukce zůstávají stejné." +
                  $" Pracujte co nejrychleji. Položte obě ruce po stranách desky. Jste připraven/a?“";
        SecondAudioInstructionViewModel = audioResolver.Get(1);
        FirstAudioInstructionViewModel = audioResolver.Get(0, true, SecondAudioInstructionViewModel);
    }

    public string TopAudioInstruction { get; }

    public string BottomAudioInstruction =>
        $"„Děkuji. Nyní, prosím, vraťte kolíky zpět do zásobníku {ReturnDirection}.“";

    public InstructionPlayerViewModel FirstAudioInstructionViewModel { get; }
    public InstructionPlayerViewModel SecondAudioInstructionViewModel { get; }

    private string CurrentHand { get; }
    private string DominantHand { get; }
    private string NonDominantHand { get; }
    private string ReturnDirection { get; }
}