using TestAdministration.Models.Data;
using TestAdministration.Models.TestBuilders;

namespace TestAdministration.ViewModels.Instructions.Nhpt;

public class NhptInstructionsViewModel(
    ITestBuilder testBuilder,
    Hand dominantHand
) : ViewModelBase, IInstructionsViewModel
{
    public ViewModelBase CurrentViewModel => (testBuilder.CurrentSection, testBuilder.CurrentTrial) switch
    {
        (0, 0) => new NhptInstructionsDominantPracticeViewModel(dominantHand),
        (1, 0) => new NhptInstructionsNonDominantPracticeViewModel(dominantHand),
        _ => new NhptInstructionsRegularViewModel(testBuilder.CurrentTrial, dominantHand),
    };
}