using TestAdministration.Models.Data;

namespace TestAdministration.ViewModels.Instructions.Ppt;

public class PptInstructionsDominantHandFirstViewModel(
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

    private string UppercaseDominantHandInstrumental => dominantHand == Hand.Right ? "Pravou" : "Levou";
    private string DominantHandLocative => dominantHand == Hand.Right ? "pravé" : "levé";
}