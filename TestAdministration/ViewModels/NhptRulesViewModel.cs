namespace TestAdministration.ViewModels;

/// <summary>
/// A view model for binding a view containing list or rules
/// for conducting Nine Hole Peg Test.
/// </summary>
public class NhptRulesViewModel : ViewModelBase
{
    public IEnumerable<string> AnnulationRules =>
    [
        "Proband vzal do ruky více než jeden kolík a sám se neopravil (nevrátil omylem uchopený kolík zpět do" +
        " zásobníku před jeho použitím).",
        "Proband si pomohl při manipulaci s kolíkem druhou rukou.",
        "Kolík upadl mimo testovací desku nebo mimo stůl (např. do klína nebo na zem).",
        "Kolík byl zastrčen do otvoru jen částečně a zůstal tak (proband ho do něj nedozastrčil aktivně).",
        "Kolík se nechtěným vyjmutím z otvoru sám dostal do zásobníku (nebyl tam aktivně přesunut pomocí prstů" +
        " probanda).",
        "Kolík se při vracení zpět do zásobníku vůbec nedotkl tohoto zásobníku a spadl na zem nebo do klína (pokus" +
        " nebyl dokončen).",
        "Proband z nějakého důvodu přestal pokračovat v provádění testu (např. vyhodnotil, že udělal chybu a měl by" +
        " nejspíše začít znova)."
    ];

    public IEnumerable<string> ContinuationRules =>
    [
        "Probandovi upadl kolík na testovací desku nebo na stůl, vzal ho testovanou rukou a pokračoval dál.",
        "Správně umístěný kolík proband z otvoru omylem vyjmul a sám ho tam znovu vrátil.",
        "Proband umístil kolík do otvoru jen částečně, ale během pokusu ho do otvoru úplně dozastrčil (sám nebo na" +
        " základě poskytnuté slovní instrukce: „Zastrčte ho!“) a pokračoval dál.",
        "Kolík se při vracení zpět do zásobníku právě od tohoto zásobníku odrazil a vyletěl mimo něj (počítá se, jako" +
        " by do něj byl správně umístěn).",
        "Kolík se při vracení zpět do zásobníku vůbec nedotkl tohoto zásobníku a spadl na stůl či testovací desku," +
        " proband ho sám uchopil testovanou rukou a umístil ho do zásobníku nebo se tento kolík zásobníku alespoň" +
        " dotkl (počítá se, jako by do něj byl správně umístěn)."
    ];
}