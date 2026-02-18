using TestAdministration.Models.Data;

namespace TestAdministration.ViewModels.Testing.Instructions.Bbt;

public class BbtInstructionsDominantPracticeViewModel : ViewModelBase, IInstructionsPageViewModel
{
    private const string ResourcesPath = "/Resources/Images";
    
    public BbtInstructionsDominantPracticeViewModel(AudioInstructionResolver audioResolver, Hand dominantHand)
    {
        DominantHand = dominantHand == Hand.Right ? "pravou" : "levou";
        
        DominantHandAcronym = dominantHand == Hand.Right ? "PHK" : "LHK";
        var imageSuffix = dominantHand == Hand.Right ? "Right" : "Left";
        ImagePath = $"{ResourcesPath}/Bbt{imageSuffix}.jpg";
        
        ThirdAudioInstructionViewModel = audioResolver.Get(1, true);
        SecondAudioInstructionViewModel = audioResolver.Get(0, true, nextPlayer: ThirdAudioInstructionViewModel);
        FirstAudioInstructionViewModel = audioResolver.GetVolumeCheck(SecondAudioInstructionViewModel);
    }

    public string DominantHandAcronym { get; }
    public string ImagePath { get; }
    public string DominantHand { get; }
    public InstructionPlayerViewModel FirstAudioInstructionViewModel { get; }
    public InstructionPlayerViewModel SecondAudioInstructionViewModel { get; }
    public InstructionPlayerViewModel ThirdAudioInstructionViewModel { get; }
}