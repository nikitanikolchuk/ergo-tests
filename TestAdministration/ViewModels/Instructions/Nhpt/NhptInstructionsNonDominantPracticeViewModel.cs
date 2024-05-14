using TestAdministration.Models.Data;

namespace TestAdministration.ViewModels.Instructions.Nhpt;

public class NhptInstructionsNonDominantPracticeViewModel : ViewModelBase, IInstructionsPageViewModel
{
    private const string ResourcesPath = "/Resources/Images";

    public NhptInstructionsNonDominantPracticeViewModel(AudioInstructionResolver audioResolver, Hand dominantHand)
    {
        NonDominantHandGenitive = dominantHand == Hand.Right ? "levé" : "pravé";
        NonDominantHandInstrumental = dominantHand == Hand.Right ? "levou" : "pravou";

        NonDominantHandAcronym = dominantHand == Hand.Right ? "LHK" : "PHK";
        var imageSuffix = dominantHand == Hand.Right ? "Left" : "Right";
        ImagePath = $"{ResourcesPath}/Nhpt{imageSuffix}.jpg";

        FirstAudioInstructionViewModel = audioResolver.Get(0, true);
    }

    public string NonDominantHandAcronym { get; }
    public string ImagePath { get; }
    public string NonDominantHandGenitive { get; }

    public string AudioInstruction =>
        $"„Nyní zopakujeme to samé s vaší {NonDominantHandInstrumental} rukou. Nejprve opět provedeme zkušební pokus." +
        $" Uchopte desku oběma rukama. Jste připraven/a?“";

    public InstructionPlayerViewModel FirstAudioInstructionViewModel { get; }

    private string NonDominantHandInstrumental { get; }
}