using TestAdministration.Models.Data;

namespace TestAdministration.ViewModels.Instructions.Bbt;

public class BbtInstructionsNonDominantPracticeViewModel : ViewModelBase, IInstructionsPageViewModel
{
    private const string ResourcesPath = "/Resources/Images";

    public BbtInstructionsNonDominantPracticeViewModel(AudioInstructionResolver audioResolver, Hand dominantHand)
    {
        NonDominantHand = dominantHand == Hand.Right ? "levou" : "pravou";

        NonDominantHandAcronym = dominantHand == Hand.Right ? "LHK" : "PHK";
        var imageSuffix = dominantHand == Hand.Right ? "Left" : "Right";
        ImagePath = $"{ResourcesPath}/Bbt{imageSuffix}.jpg";

        FirstAudioInstructionViewModel = audioResolver.Get(0);
    }

    public string NonDominantHandAcronym { get; }
    public string ImagePath { get; }
    public string NonDominantHand { get; }
    public InstructionPlayerViewModel FirstAudioInstructionViewModel { get; }
}