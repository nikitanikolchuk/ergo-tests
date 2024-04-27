using TestAdministration.Models.Data;

namespace TestAdministration.ViewModels.Instructions.Bbt;

public class BbtInstructionsDominantPracticeViewModel(
    Hand dominantHand
) : ViewModelBase
{
    public string TopOralInstruction =>
        $"„Chci zjistit, jak rychle dokážete vaší {DominantHand} rukou (ukažte na danou ruku) chytit postupně jednu" +
        $" kostku podruhé, přenést ji přes přepážku na druhou stranu testovací krabice a pustit ji. Je důležité," +
        $" abyste se dostal/a konečky vašich prstů až za přepážku. Sledujte mě. Ukážu vám jak.“";

    private string DominantHand => dominantHand == Hand.Right ? "pravou" : "levou";
}