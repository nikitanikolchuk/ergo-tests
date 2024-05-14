using TestAdministration.Models.Data;

namespace TestAdministration.ViewModels.Instructions.Ppt;

public class PptInstructionsDominantHandFirstViewModel : ViewModelBase, IInstructionsPageViewModel
{
    private const string ResourcesPath = "/Resources/Images";

    public PptInstructionsDominantHandFirstViewModel(AudioInstructionResolver audioResolver, Hand dominantHand)
    {
        DominantHandInstrumental = dominantHand == Hand.Right ? "pravou" : "levou";
        DominantHandGenitive = dominantHand == Hand.Right ? "pravého" : "levého";
        PinsSide = dominantHand == Hand.Right ? "vpravo" : "nalevo";
        WashersSide = dominantHand == Hand.Right ? "nalevo" : "vpravo";
        CollarsSide = dominantHand == Hand.Right ? "vpravo" : "nalevo";
        UppercaseDominantHandInstrumental = dominantHand == Hand.Right ? "Pravou" : "Levou";
        DominantHandLocative = dominantHand == Hand.Right ? "pravé" : "levé";
        DominantHandPlural = dominantHand == Hand.Right ? "praváky" : "leváky";

        var imageSuffix = dominantHand == Hand.Right ? "Right" : "Left";
        ImagePath = $"{ResourcesPath}/Ppt{imageSuffix}.jpg";

        FifthAudioInstructionViewModel = audioResolver.Get(4);
        FourthAudioInstructionViewModel = audioResolver.Get(3, true, FifthAudioInstructionViewModel);
        ThirdAudioInstructionViewModel = audioResolver.Get(2, false, FourthAudioInstructionViewModel);
        SecondAudioInstructionViewModel = audioResolver.Get(1, false, ThirdAudioInstructionViewModel);
        FirstAudioInstructionViewModel = audioResolver.Get(0, true, SecondAudioInstructionViewModel);
    }

    public string DominantHandInstrumental { get; }
    public string PinsSide { get; }
    public string WashersSide { get; }
    public string CollarsSide { get; }
    public string DominantHandPlural { get; }
    public string ImagePath { get; }

    public string PracticeAudioInstruction =>
        $"„{UppercaseDominantHandInstrumental} rukou vezměte vždy jeden kolík z {DominantHandGenitive} zásobníku." +
        $" Jednotlivé kolíky umisťujte do řady {PinsSide}. Začněte horním otvorem.“";

    public string TrialAudioInstruction =>
        $"„Až řeknu: „Teď!“, umístěte co nejvíce kolíků do řady na {DominantHandLocative} straně. Začněte horním" +
        $" otvorem. Pracujte co nejrychleji dokážete, dokud neřeknu: „Stop!“. Položte obě ruce po stranách desky." +
        $" Jste připraven/a?“";

    public InstructionPlayerViewModel FirstAudioInstructionViewModel { get; }
    public InstructionPlayerViewModel SecondAudioInstructionViewModel { get; }
    public InstructionPlayerViewModel ThirdAudioInstructionViewModel { get; }
    public InstructionPlayerViewModel FourthAudioInstructionViewModel { get; }
    public InstructionPlayerViewModel FifthAudioInstructionViewModel { get; }

    private string UppercaseDominantHandInstrumental { get; }
    private string DominantHandGenitive { get; }
    private string DominantHandLocative { get; }
}