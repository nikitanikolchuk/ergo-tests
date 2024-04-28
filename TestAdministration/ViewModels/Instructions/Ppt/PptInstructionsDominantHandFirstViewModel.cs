using TestAdministration.Models.Data;

namespace TestAdministration.ViewModels.Instructions.Ppt;

public class PptInstructionsDominantHandFirstViewModel(
    AudioInstructionResolver audioResolver,
    Hand dominantHand
) : ViewModelBase
{
    public string DominantHandInstrumental => dominantHand == Hand.Right ? "pravou" : "levou";
    public string WashersSide => dominantHand == Hand.Right ? "nalevo" : "napravo";
    public string CollarsSide => dominantHand == Hand.Right ? "napravo" : "nalevo";

    public string PracticeAudioInstruction =>
        $"„{UppercaseDominantHandInstrumental} rukou vezměte vždy jeden kolík z pravého zásobníku. Jednotlivé kolíky" +
        $" umisťujte do řady napravo. Začněte horním otvorem.“";

    public string TrialAudioInstruction =>
        $"„Až řeknu: „Teď!“, umístěte co nejvíce kolíků do řady na {DominantHandLocative} straně. Začněte horním" +
        $" otvorem. Pracujte co nejrychleji dokážete, dokud neřeknu: „Stop!“. Položte obě ruce po stranách desky." +
        $" Jste připraven/a?“";

    public ViewModelBase FirstAudioInstructionViewModel => audioResolver.Get(0, true);
    public ViewModelBase SecondAudioInstructionViewModel => audioResolver.Get(1);
    public ViewModelBase ThirdAudioInstructionViewModel => audioResolver.Get(2);
    public ViewModelBase FourthAudioInstructionViewModel => audioResolver.Get(3, true);
    public ViewModelBase FifthAudioInstructionViewModel => audioResolver.Get(4);

    private string UppercaseDominantHandInstrumental => dominantHand == Hand.Right ? "Pravou" : "Levou";
    private string DominantHandLocative => dominantHand == Hand.Right ? "pravé" : "levé";
}