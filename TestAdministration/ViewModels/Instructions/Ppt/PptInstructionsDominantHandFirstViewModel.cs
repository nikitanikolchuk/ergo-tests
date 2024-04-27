using TestAdministration.Models.Data;

namespace TestAdministration.ViewModels.Instructions.Ppt;

public class PptInstructionsDominantHandFirstViewModel(
    Hand dominantHand
) : ViewModelBase
{
    public string TopText =>
        $"Vyzvěte probanda, ať se posadí ke stolu na pevnou židli. Sami se posaďte naproti němu. Výšku stolu" +
        $" nastavte tak, aby na něm mohl mít položenou alespoň polovinu předloktí za vzpřímeného sedu. Desku umístěte" +
        $" na stůl tak, aby řada čtyř zásobníků byla v horní části desky (odsuňte kryt s názvem testu). Zarovnejte" +
        $" spodní hranu desky s okrajem stolu a střed desky přibližně se středem trupu probanda. Každý ze zásobníků" +
        $" úplně vpravo a úplně vlevo by měl obsahovat 25 kolíků (celkový počet 50 kolíků). Pro probandy s dominantní" +
        $" {DominantHandInstrumental} rukou bude v zásobníku {WashersSide} od středu umístěno 40 podložek a" +
        $" {CollarsSide} od středu 40 trubiček.";

    public string PracticeOralInstruction =>
        $"„{UppercaseDominantHandInstrumental} rukou vezměte vždy jeden kolík z pravého zásobníku. Jednotlivé kolíky" +
        $" umisťujte do řady napravo. Začněte horním otvorem.“";

    public string TrialOralInstruction =>
        $"„Až řeknu: „Teď!“, umístěte co nejvíce kolíků do řady na {DominantHandLocative} straně. Začněte horním" +
        $" otvorem. Pracujte co nejrychleji dokážete, dokud neřeknu: „Stop!“. Položte obě ruce po stranách desky." +
        $" Jste připraven/a?“";

    private string DominantHandInstrumental => dominantHand == Hand.Right ? "pravou" : "levou";
    private string WashersSide => dominantHand == Hand.Right ? "nalevo" : "napravo";
    private string CollarsSide => dominantHand == Hand.Right ? "napravo" : "nalevo";
    private string UppercaseDominantHandInstrumental => dominantHand == Hand.Right ? "Pravou" : "Levou";
    private string DominantHandLocative => dominantHand == Hand.Right ? "pravé" : "levé";
}