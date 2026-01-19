using TestAdministration.Models.Data;

namespace TestAdministration.ViewModels.Testing.Instructions.Nhpt;

public class NhptInstructionsDominantPracticeViewModel : ViewModelBase, IInstructionsPageViewModel
{
    private const string ResourcesPath = "/Resources/Images";

    public NhptInstructionsDominantPracticeViewModel(
        AudioInstructionResolver audioResolver,
        Hand dominantHand,
        int totalTrialCount
    )
    {
        DominantHand = dominantHand == Hand.Right ? "pravou" : "levou";
        NonDominantHand = dominantHand == Hand.Right ? "levou" : "pravou";

        var trialCount = totalTrialCount - 1;

        TrialCount = trialCount switch
        {
            2 => " Každá ruka bude testována dvakrát.",
            3 => " Každá ruka bude testována třikrát.",
            _ => string.Empty
        };

        DominantHandAcronym = dominantHand == Hand.Right ? "PHK" : "LHK";
        var imageSuffix = dominantHand == Hand.Right ? "Right" : "Left";
        ImagePath = $"{ResourcesPath}/Nhpt{imageSuffix}.jpg";

        ThirdAudioInstructionViewModel = audioResolver.Get(2, true);
        SecondAudioInstructionViewModel = audioResolver.Get(1, nextPlayer: ThirdAudioInstructionViewModel);
        FirstAudioInstructionViewModel = audioResolver.Get(0, trialCount: trialCount, nextPlayer: SecondAudioInstructionViewModel);
    }

    public string TopText =>
        $"Nejdříve otestujte dominantní ({DominantHand}) ruku. Probandovi sdělte následující instrukce/přehrajte" +
        $" nahrávku:";

    public string FirstAudioInstruction =>
        $"„Chci zjistit, jak rychle a přesně dokážete provést tento Devítikolíkový test. Nejprve budete pracovat vaší" +
        $" {DominantHand} rukou, pak tou {NonDominantHand}.{TrialCount} Vždy vám nejprve" +
        $" řeknu, co máte dělat, a pak budete mít příležitost si to ještě vyzkoušet nanečisto. Desku si budete" +
        $" přidržovat {NonDominantHand} rukou. Postupně budete po jednom odebírat kolíky ze zásobníku pouze" +
        $" {DominantHand} rukou a dávat je do otvorů v libovolném pořadí, dokud nebudou všechny otvory zaplněny.“";

    public string DominantHandAcronym { get; }
    public string ImagePath { get; }

    public InstructionPlayerViewModel FirstAudioInstructionViewModel { get; }
    public InstructionPlayerViewModel SecondAudioInstructionViewModel { get; }
    public InstructionPlayerViewModel ThirdAudioInstructionViewModel { get; }

    private string DominantHand { get; }
    private string NonDominantHand { get; }
    private string TrialCount { get; }
}