namespace TestAdministration.ViewModels.Instructions.Ppt;

public class PptInstructionsBothHandsRegularViewModel(
    AudioInstructionResolver audioResolver,
    int trial
) : ViewModelBase
{
    public string FirstAudioInstruction =>
        trial == 1
            ? "„Teď ještě jednou zopakujeme to samé oběma rukama najednou. Instrukce zůstávají stejné. Pracujte co" +
              " nejrychleji. Položte obě ruce po stranách desky. Jste připraven/a?“"
            : "„Teď ještě naposledy zopakujeme to samé oběma rukama najednou. Instrukce zůstávají stejné. Pracujte" +
              " co nejrychleji. Položte obě ruce po stranách desky. Jste připraven/a?“";

    public ViewModelBase FirstAudioInstructionViewModel => audioResolver.Get(0, true);
    public ViewModelBase SecondAudioInstructionViewModel => audioResolver.Get(1);
}