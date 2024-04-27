using TestAdministration.Models.Data;

namespace TestAdministration.ViewModels.Instructions.Ppt;

public class PptInstructionsBothHandsFirstViewModel(
    Hand dominantHand
) : ViewModelBase
{
    public string OralInstruction =>
        $"„V této části testu budete používat obě ruce zároveň. {OrderedInstruction} Poté oba kolíky umístěte do řad." +
        $" Začněte horním otvorem v obou řadách.";

    private string OrderedInstruction =>
        dominantHand == Hand.Right
            ? "Pravou rukou vezměte kolík z pravého zásobníku a zároveň vezměte kolík z levého zásobníku levou rukou."
            : "Levou rukou vezměte kolík z levého zásobníku a zároveň vezměte kolík z pravého zásobníku pravou rukou.";
}