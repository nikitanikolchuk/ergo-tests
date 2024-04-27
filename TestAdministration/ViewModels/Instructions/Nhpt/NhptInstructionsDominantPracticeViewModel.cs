using TestAdministration.Models.Data;

namespace TestAdministration.ViewModels.Instructions.Nhpt;

public class NhptInstructionsDominantPracticeViewModel(
    Hand dominantHand
) : ViewModelBase
{
    public string TopText =>
        $"Nejdříve otestujte dominantní ({DominantHand}) ruku. Probandovi sdělte následující instrukce/přehrajte" +
        $" nahrávku:";

    public string TopOralInstruction =>
        $"„Chci zjistit, jak rychle a přesně dokážete provést tento Devítikolíkový test. Nejprve budete pracovat vaší" +
        $" {DominantHand} rukou, pak tou {NonDominantHand}. Každá ruka bude testována třikrát. Vždy vám nejprve" +
        $" řeknu, co máte dělat, a pak budete mít příležitost si to ještě vyzkoušet nanečisto. Desku si budete" +
        $" přidržovat {DominantHand} rukou. Postupně budete po jednom odebírat kolíky ze zásobníku pouze" +
        $" {DominantHand} rukou a dávat je do otvorů v libovolném pořadí, dokud nebudou všechny otvory zaplněny.“";

    private string DominantHand => dominantHand == Hand.Right ? "pravou" : "levou";
    private string NonDominantHand => dominantHand == Hand.Right ? "levou" : "rukou";
}