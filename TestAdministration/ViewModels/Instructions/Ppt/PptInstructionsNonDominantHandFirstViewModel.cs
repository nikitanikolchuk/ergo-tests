using TestAdministration.Models.Data;

namespace TestAdministration.ViewModels.Instructions.Ppt;

public class PptInstructionsNonDominantHandFirstViewModel(
    AudioInstructionResolver audioResolver,
    Hand dominantHand
) : ViewModelBase
{
    public string PracticeAudioInstruction =>
        $"„{NonDominantHandInstrumental} rukou vezměte vždy jeden kolík z levého zásobníku. Jednotlivé kolíky" +
        $" umisťujte do řady nalevo. Začněte horním otvorem.“";

    public string TrialAudioInstruction =>
        $"„Až řeknu: „Teď!“, umístěte co nejvíce kolíků do řady na {NonDominantHandLocative} straně, začněte horním" +
        $" otvorem. Pracujte co nejrychleji dokážete, dokud neřeknu: „Stop!“. Položte obě ruce po stranách desky." +
        $" Jste připraven/a?“";

    public string LastAudioInstruction =>
        $"„Děkuji. Nyní, prosím, vraťte kolíky zpět do zásobníku {ReturnLocation}.“";

    public ViewModelBase FirstAudioInstructionViewModel => audioResolver.Get(0);
    public ViewModelBase SecondAudioInstructionViewModel => audioResolver.Get(1);
    public ViewModelBase ThirdAudioInstructionViewModel => audioResolver.Get(2, true);
    public ViewModelBase FourthAudioInstructionViewModel => audioResolver.Get(3);

    private string NonDominantHandInstrumental => dominantHand == Hand.Right ? "Levou" : "Pravou";
    private string NonDominantHandLocative => dominantHand == Hand.Right ? "levé" : "pravé";
    private string ReturnLocation => dominantHand == Hand.Right ? "nalevo" : "napravo";
}