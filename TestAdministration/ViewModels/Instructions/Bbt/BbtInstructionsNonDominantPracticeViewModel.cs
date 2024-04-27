using TestAdministration.Models.Data;

namespace TestAdministration.ViewModels.Instructions.Bbt;

public class BbtInstructionsNonDominantPracticeViewModel(
    Hand dominantHand
) : ViewModelBase
{
    public string OralInstruction =>
        $"„Nyní budete provádět totéž, a to {NonDominantHand} rukou (ukažte na danou ruku). Před testem si to opět" +
        $" nejprve vyzkoušíte. Postupně budete rukou odebírat jednotlivé kostky a umisťovat je na druhou stranu" +
        $" krabice. Nezapomeňte, že kostka nebude započítána, pokud ji přemístíte, ale nedostanete se konečky vašich" +
        $" prstů přes přepážku. Položte obě ruce po stranách krabice.“";

    private string NonDominantHand => dominantHand == Hand.Right ? "levou" : "pravou";
}