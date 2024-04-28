namespace TestAdministration.ViewModels.Instructions.Ppt;

public class PptInstructionsAssemblyRegularViewModel(
    AudioInstructionResolver audioResolver,
    int trial
) : ViewModelBase
{
    public string AudioInstruction =>
        trial == 1
            ? "„Teď ještě jednou zopakujeme to samé. Instrukce zůstávají stejné. Pracujte co nejrychleji. Položte obě" +
              " ruce po stranách desky. Jste připraven/a?“"
            : "„Teď ještě naposledy zopakujeme to samé. Instrukce zůstávají stejné. Pracujte co nejrychleji. Položte" +
              " obě ruce po stranách desky. Jste připraven/a?“";

    public ViewModelBase FirstAudioInstructionViewModel => audioResolver.Get(0, true);
    public ViewModelBase SecondAudioInstructionViewModel => audioResolver.Get(1);
}