using TestAdministration.Models.Data;
using TestAdministration.Models.TestBuilders;

namespace TestAdministration.ViewModels.Instructions.Bbt;

public class BbtInstructionsViewModel(
    ITestBuilder testBuilder,
    Hand dominantHand
) : ViewModelBase, IInstructionsViewModel
{
    public ViewModelBase CurrentViewModel => (testBuilder.CurrentSection, testBuilder.CurrentTrial) switch
    {
        (0, 0) => new BbtInstructionsDominantPracticeViewModel(dominantHand),
        (1, 0) => new BbtInstructionsNonDominantPracticeViewModel(dominantHand),
        _ => new BbtInstructionsRegularViewModel(
            testBuilder.CurrentSection,
            testBuilder.CurrentTrial,
            dominantHand
        )
    };
}