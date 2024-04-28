using TestAdministration.Models.Data;

namespace TestAdministration.ViewModels.Instructions.Ppt;

public class PptInstructionsAssemblyFirstViewModel(
    AudioInstructionResolver audioResolver,
    Hand dominantHand
) : ViewModelBase
{
    public string FirstAudioInstruction =>
        $"„Pravou rukou vezměte jeden kolík z {DominantHandMasculine} zásobníku. Zatímco jej budete umisťovat do" +
        $" horního otvoru {DominantHandFeminine} řady, uchopte {NonDominantHandInstrumental} rukou podložku. Jakmile" +
        $" umístíte kolík, nasaďte podložku na kolík. Zatímco budete {NonDominantHandInstrumental} rukou nasazovat" +
        $" podložku na kolík, uchopte {DominantHandInstrumental} rukou trubičku. Zatímco budete nasazovat trubičku na" +
        $" kolík, uchopte {NonDominantHandInstrumental} rukou další podložku a nasaďte ji na trubičku. Tím dokončíte" +
        $" první “komplet“ složený z kolíku, podložky, trubičky a podložky.";

    public string SecondAudioInstruction =>
        $"Zatímco budete {NonDominantHandInstrumental} rukou nasazovat poslední podložku prvního kompletu, začněte" +
        $" okamžitě vytvářet další komplet tím, že {DominantHandInstrumental} rukou vezmete další kolík. Umístěte" +
        $" jej do dalšího otvoru, {NonDominantHandInstrumental} rukou nasaďte podložku a tak dále, až dokončíte další" +
        $" komplet“";

    public string TrialAudioInstruction =>
        $"Až řeknu: „Teď!“, začněte vytvářet co nejvíce kompletů. Začněte horním otvorem v {DominantHandFeminine}" +
        $" řadě. Pracujte co nejrychleji, dokud neřeknu: „Stop!“. Položte obě ruce po stranách desky. Jste" +
        $" připraven/a?“";

    public ViewModelBase FirstAudioInstructionViewModel => audioResolver.Get(0);
    public ViewModelBase SecondAudioInstructionViewModel => audioResolver.Get(1);
    public ViewModelBase ThirdAudioInstructionViewModel => audioResolver.Get(2, true);
    public ViewModelBase FourthAudioInstructionViewModel => audioResolver.Get(3);

    private string DominantHandMasculine => dominantHand == Hand.Right ? "pravého" : "levého";
    private string DominantHandFeminine => dominantHand == Hand.Right ? "pravé" : "levé";
    private string NonDominantHandInstrumental => dominantHand == Hand.Right ? "levou" : "pravou";
    private string DominantHandInstrumental => dominantHand == Hand.Right ? "pravou" : "levou";
}