using TestAdministration.Models.Data;

namespace TestAdministration.ViewModels.Instructions.Bbt;

public class BbtInstructionsDominantPracticeViewModel : ViewModelBase, IInstructionsPageViewModel
{
    private const string ResourcesPath = "/Resources/Images";
    
    public BbtInstructionsDominantPracticeViewModel(AudioInstructionResolver audioResolver, Hand dominantHand)
    {
        DominantHand = dominantHand == Hand.Right ? "pravou" : "levou";
        
        DominantHandAcronym = dominantHand == Hand.Right ? "PHK" : "LHK";
        var imageSuffix = dominantHand == Hand.Right ? "Right" : "Left";
        ImagePath = $"{ResourcesPath}/Bbt{imageSuffix}.jpg";
        
        SecondAudioInstructionViewModel = audioResolver.Get(1, true);
        FirstAudioInstructionViewModel = audioResolver.Get(0, true, SecondAudioInstructionViewModel);
    }

    public string DominantHandAcronym { get; }
    public string ImagePath { get; }
    public string DominantHand { get; }
    public InstructionPlayerViewModel FirstAudioInstructionViewModel { get; }
    public InstructionPlayerViewModel SecondAudioInstructionViewModel { get; }
}