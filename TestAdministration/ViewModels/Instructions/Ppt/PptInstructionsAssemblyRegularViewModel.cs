namespace TestAdministration.ViewModels.Instructions.Ppt;

public class PptInstructionsAssemblyRegularViewModel(
    int trial
) : ViewModelBase
{
    public string AudioInstruction =>
        trial == 2
            ? "„Teď ještě jednou zopakujeme to samé. Instrukce zůstávají stejné. Pracujte co nejrychleji. Položte obě" +
              " ruce po stranách desky. Jste připraven/a?“"
            : "„Teď ještě naposledy zopakujeme to samé. Instrukce zůstávají stejné. Pracujte co nejrychleji. Položte" +
              " obě ruce po stranách desky. Jste připraven/a?“";
}