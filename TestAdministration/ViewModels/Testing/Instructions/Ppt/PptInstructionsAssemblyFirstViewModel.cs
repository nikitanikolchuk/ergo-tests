using TestAdministration.Models.Data;

namespace TestAdministration.ViewModels.Testing.Instructions.Ppt;

public class PptInstructionsAssemblyFirstViewModel : ViewModelBase, IInstructionsPageViewModel
{
    public PptInstructionsAssemblyFirstViewModel(
        AudioInstructionResolver audioResolver,
        Hand dominantHand,
        int trialCount
    )
    {
        DominantHandMasculine = dominantHand == Hand.Right ? "pravého" : "levého";
        DominantHandFeminine = dominantHand == Hand.Right ? "pravé" : "levé";
        NonDominantHandInstrumental = dominantHand == Hand.Right ? "levou" : "pravou";
        DominantHandInstrumental = dominantHand == Hand.Right ? "pravou" : "levou";
        FollowTheSameWay = trialCount switch
        {
            1 => string.Empty,
            2 => "Stejným způsobem postupujte i u druhého pokusu tohoto subtestu.",
            _ => "Stejným způsobem postupujte i u dalších pokusů tohoto subtestu."
        };
        FourthAudioInstructionViewModel = audioResolver.Get(3);
        ThirdAudioInstructionViewModel = audioResolver.Get(2, true, nextPlayer: FourthAudioInstructionViewModel);
        SecondAudioInstructionViewModel = audioResolver.Get(1, nextPlayer: ThirdAudioInstructionViewModel);
        FirstAudioInstructionViewModel = audioResolver.Get(0, nextPlayer: SecondAudioInstructionViewModel);
    }

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

    public string FollowTheSameWay { get; }

    public InstructionPlayerViewModel FirstAudioInstructionViewModel { get; }
    public InstructionPlayerViewModel SecondAudioInstructionViewModel { get; }
    public InstructionPlayerViewModel ThirdAudioInstructionViewModel { get; }
    public InstructionPlayerViewModel FourthAudioInstructionViewModel { get; }

    private string DominantHandMasculine { get; }
    private string DominantHandFeminine { get; }
    private string NonDominantHandInstrumental { get; }
    private string DominantHandInstrumental { get; }
}