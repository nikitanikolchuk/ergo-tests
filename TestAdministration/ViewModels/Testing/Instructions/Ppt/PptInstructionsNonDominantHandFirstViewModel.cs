using TestAdministration.Models.Data;

namespace TestAdministration.ViewModels.Testing.Instructions.Ppt;

public class PptInstructionsNonDominantHandFirstViewModel : ViewModelBase, IInstructionsPageViewModel
{
    public PptInstructionsNonDominantHandFirstViewModel(
        AudioInstructionResolver audioResolver,
        Hand dominantHand,
        int trialCount
    )
    {
        NonDominantHandInstrumental = dominantHand == Hand.Right ? "Levou" : "Pravou";
        NonDominantHandLocative = dominantHand == Hand.Right ? "levé" : "pravé";
        FollowTheSameWay = trialCount switch
        {
            1 => string.Empty,
            2 => "Stejným způsobem postupujte i u druhého pokusu tohoto subtestu.",
            _ => "Stejným způsobem postupujte i u dalších pokusů tohoto subtestu."
        };
        ReturnLocation = dominantHand == Hand.Right ? "nalevo" : "vpravo";
        FourthAudioInstructionViewModel = audioResolver.Get(3);
        ThirdAudioInstructionViewModel = audioResolver.Get(2, true, nextPlayer: FourthAudioInstructionViewModel);
        SecondAudioInstructionViewModel = audioResolver.Get(1, false, nextPlayer: ThirdAudioInstructionViewModel);
        FirstAudioInstructionViewModel = audioResolver.Get(0, false, nextPlayer: SecondAudioInstructionViewModel);
    }

    public string PracticeAudioInstruction =>
        $"„{NonDominantHandInstrumental} rukou vezměte vždy jeden kolík z levého zásobníku. Jednotlivé kolíky" +
        $" umisťujte do řady nalevo. Začněte horním otvorem.“";

    public string TrialAudioInstruction =>
        $"„Až řeknu: „Teď!“, umístěte co nejvíce kolíků do řady na {NonDominantHandLocative} straně, začněte horním" +
        $" otvorem. Pracujte co nejrychleji dokážete, dokud neřeknu: „Stop!“. Položte obě ruce po stranách desky." +
        $" Jste připraven/a?“";

    public string FollowTheSameWay { get; }

    public string LastAudioInstruction =>
        $"„Děkuji. Nyní, prosím, vraťte kolíky zpět do zásobníku {ReturnLocation}.“";

    public InstructionPlayerViewModel FirstAudioInstructionViewModel { get; }
    public InstructionPlayerViewModel SecondAudioInstructionViewModel { get; }
    public InstructionPlayerViewModel ThirdAudioInstructionViewModel { get; }
    public InstructionPlayerViewModel FourthAudioInstructionViewModel { get; }

    private string NonDominantHandInstrumental { get; }
    private string NonDominantHandLocative { get; }
    private string ReturnLocation { get; }
}