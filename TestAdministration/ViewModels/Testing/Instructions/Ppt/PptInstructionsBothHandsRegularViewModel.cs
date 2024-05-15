namespace TestAdministration.ViewModels.Testing.Instructions.Ppt;

public class PptInstructionsBothHandsRegularViewModel : ViewModelBase, IInstructionsPageViewModel
{
    public PptInstructionsBothHandsRegularViewModel(
        AudioInstructionResolver audioResolver,
        int trial
    )
    {
        FirstAudioInstruction =
            trial == 1
                ? "„Teď ještě jednou zopakujeme to samé oběma rukama najednou. Instrukce zůstávají stejné. Pracujte co" +
                  " nejrychleji. Položte obě ruce po stranách desky. Jste připraven/a?“"
                : "„Teď ještě naposledy zopakujeme to samé oběma rukama najednou. Instrukce zůstávají stejné. Pracujte" +
                  " co nejrychleji. Položte obě ruce po stranách desky. Jste připraven/a?“";
        SecondAudioInstructionViewModel = audioResolver.Get(1);
        FirstAudioInstructionViewModel = audioResolver.Get(0, true, SecondAudioInstructionViewModel);
    }

    public string FirstAudioInstruction { get; }

    public InstructionPlayerViewModel FirstAudioInstructionViewModel { get; }
    public InstructionPlayerViewModel SecondAudioInstructionViewModel { get; }
}