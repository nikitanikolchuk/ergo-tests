using TestAdministration.Models.Data;
using TestAdministration.Models.TestBuilders;

namespace TestAdministration.ViewModels.Instructions.Ppt;

public class PptInstructionsViewModel(
    ITestBuilder testBuilder,
    Hand dominantHand
) : ViewModelBase, IInstructionsViewModel
{
    public ViewModelBase CurrentViewModel => (testBuilder.CurrentSection, testBuilder.CurrentTrial) switch
    {
        (0, 0) => new PptInstructionsDominantHandFirstViewModel(dominantHand),
        (1, 0) => new PptInstructionsNonDominantHandFirstViewModel(dominantHand),
        (0, _) => new PptInstructionsSingleHandRegularViewModel(testBuilder.CurrentTrial, dominantHand),
        (1, _) => new PptInstructionsSingleHandRegularViewModel(testBuilder.CurrentTrial, dominantHand),
        (2, 0) => new PptInstructionsBothHandsFirstViewModel(dominantHand),
        (2, _) => new PptInstructionsBothHandsRegularViewModel(),
        (3, 0) => new PptInstructionsAssemblyFirstViewModel(dominantHand),
        _ => new PptInstructionsAssemblyRegularViewModel(testBuilder.CurrentTrial)
    };
}