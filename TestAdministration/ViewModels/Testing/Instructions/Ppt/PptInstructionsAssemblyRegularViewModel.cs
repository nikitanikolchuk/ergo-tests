namespace TestAdministration.ViewModels.Testing.Instructions.Ppt;

public class PptInstructionsAssemblyRegularViewModel : ViewModelBase, IInstructionsPageViewModel
{
    public PptInstructionsAssemblyRegularViewModel(
        AudioInstructionResolver audioResolver,
        int trial,
        int trialCount
    )
    {
        AudioInstruction =
            trial != trialCount - 1
                ? "„Teď ještě jednou zopakujeme to samé. Instrukce zůstávají stejné. Pracujte co nejrychleji. Položte obě" +
                  " ruce po stranách desky. Jste připraven/a?“"
                : "„Teď ještě naposledy zopakujeme to samé. Instrukce zůstávají stejné. Pracujte co nejrychleji. Položte" +
                  " obě ruce po stranách desky. Jste připraven/a?“";
        SecondAudioInstructionViewModel = audioResolver.Get(1);
        FirstAudioInstructionViewModel = audioResolver.Get(0, true, nextPlayer: SecondAudioInstructionViewModel);
    }

    public string AudioInstruction { get; }

    public InstructionPlayerViewModel FirstAudioInstructionViewModel { get; }
    public InstructionPlayerViewModel SecondAudioInstructionViewModel { get; }
}