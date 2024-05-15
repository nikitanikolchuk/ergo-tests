using TestAdministration.Models.Data;

namespace TestAdministration.ViewModels.Testing.Instructions.Ppt;

public class PptInstructionsBothHandsFirstViewModel : ViewModelBase, IInstructionsPageViewModel
{
    public PptInstructionsBothHandsFirstViewModel(AudioInstructionResolver audioResolver, Hand dominantHand)
    {
        OrderedInstruction = dominantHand == Hand.Right
            ? "Pravou rukou vezměte kolík z pravého zásobníku a zároveň vezměte kolík z levého zásobníku levou rukou."
            : "Levou rukou vezměte kolík z levého zásobníku a zároveň vezměte kolík z pravého zásobníku pravou rukou.";
        FourthAudioInstructionViewModel = audioResolver.Get(3);
        ThirdAudioInstructionViewModel = audioResolver.Get(2, true, FourthAudioInstructionViewModel);
        SecondAudioInstructionViewModel = audioResolver.Get(1, false, ThirdAudioInstructionViewModel);
        FirstAudioInstructionViewModel = audioResolver.Get(0, false, SecondAudioInstructionViewModel);
    }

    public string AudioInstruction =>
        $"„V této části testu budete používat obě ruce zároveň. {OrderedInstruction} Poté oba kolíky umístěte do řad." +
        $" Začněte horním otvorem v obou řadách.";

    public InstructionPlayerViewModel FirstAudioInstructionViewModel { get; }
    public InstructionPlayerViewModel SecondAudioInstructionViewModel { get; }
    public InstructionPlayerViewModel ThirdAudioInstructionViewModel { get; }
    public InstructionPlayerViewModel FourthAudioInstructionViewModel { get; }

    private string OrderedInstruction { get; }
}