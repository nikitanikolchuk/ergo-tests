namespace TestAdministration.ViewModels.Rules;

/// <summary>
/// A view model for binding a view containing list or rules
/// for conducting Box and Block Test.
/// </summary>
public class BbtRulesViewModel : ViewModelBase
{
    public IEnumerable<string> AnnulationRules =>
    [
        "Výkon probanda během testování významně ovlivnily rušivé faktory prostředí."
    ];

    public IEnumerable<string> BlockCountedRules =>
    [
        "Testovaná osoba se dotkla přepážky.",
        "Byla manipulace s kostkou provedena patologickým způsobem."
    ];

    public IEnumerable<string> BlockNotCountedRules =>
    [
        "Kostka byla přemístěna s dopomocí jiné části těla, než je testovaná horní končetina.",
        "Kostka se odrazila od přepážky zpět do původní přihrádky (nebyla přemístěna přes přepážku).",
        "Kostka byla přemístěna současně s další kostkou / s dalšími kostkami (Pokud bylo najednou přemístěno více" +
        " kostek, započítává se z nich pouze jedna kostka).",
        "Kostka byla přemístěna přes přepážku, ale nebyla uchopena samostatně těsně před jejím přemístěním do druhé" +
        " přihrádky (Pokud tedy proband vzal dvě kostky najednou, přehodil pouze jednu z nich a s druhou se vrátil" +
        " zpět nad původní přihrádku, drženou kostku nepustil a následně ji přehodil přes přepážku, započítáme pouze" +
        " první kostku, druhou nikoliv)."
    ];
}