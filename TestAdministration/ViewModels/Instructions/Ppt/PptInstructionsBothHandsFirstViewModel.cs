using TestAdministration.Models.Data;

namespace TestAdministration.ViewModels.Instructions.Ppt;

public class PptInstructionsBothHandsFirstViewModel(
    AudioInstructionResolver audioResolver,
    Hand dominantHand
) : ViewModelBase
{
    public string AudioInstruction =>
        $"„V této části testu budete používat obě ruce zároveň. {OrderedInstruction} Poté oba kolíky umístěte do řad." +
        $" Začněte horním otvorem v obou řadách.";

    public ViewModelBase FirstAudioInstructionViewModel => audioResolver.Get(0);
    public ViewModelBase SecondAudioInstructionViewModel => audioResolver.Get(1);
    public ViewModelBase ThirdAudioInstructionViewModel => audioResolver.Get(2, true);
    public ViewModelBase FourthAudioInstructionViewModel => audioResolver.Get(3);

    private string OrderedInstruction =>
        dominantHand == Hand.Right
            ? "Pravou rukou vezměte kolík z pravého zásobníku a zároveň vezměte kolík z levého zásobníku levou rukou."
            : "Levou rukou vezměte kolík z levého zásobníku a zároveň vezměte kolík z pravého zásobníku pravou rukou.";
}