namespace TestAdministration.ViewModels.Rules;

/// <summary>
/// A view model for binding a view containing list or rules
/// for conducting Purdue Pegboard Test.
/// </summary>
public class PptRulesViewModel : ViewModelBase
{
    public IEnumerable<PptRuleRow> CommonRules =>
    [
        new PptRuleRow(
            "Proband vzal více součástek najednou a ihned je použil.",
            "Ihned slovně zareagujte: „Po jednom!“. Při dalším opakování této chyby ihned přerušte pokus," +
            " krátce to probandovi vysvětlete a začněte ho znovu.",
            "Nepočítejte součástky, které nebral jednotlivě (např. pokud vzal dvě najednou, započítejte jen" +
            " jednu správně umístěnou součástku). Výsledky z anulovaného pokusu nepočítejte."
        ),
        new PptRuleRow(
            "Proband vzal více součástek najednou, ale sám se ihned opravil a nadbytečné součástky vrátil" +
            " zpět.",
            "Nechte pokus mlčky běžet dál.",
            "Správně umístěnou součástku normálně započítejte."
        ),
        new PptRuleRow(
            "Součástka upadla na testovací desku do prostoru se zásobníky a proband ji znovu uchopil a použil.",
            "Nechte pokus mlčky běžet dál.",
            "Umístěnou součástku normálně započítejte."
        ),
        new PptRuleRow(
            "Součástka upadla kamkoliv mimo prostor se zásobníky a proband ji znovu uchopil a použil.",
            "Ihned slovně zareagujte: „Novou součástku si vezměte!“.",
            "Nepočítejte umístěnou součástku uchopenou z jiného místa, než je prostor se zásobníky."
        ),
        new PptRuleRow(
            "Proband neumisťuje kolíky do nejbližších otvorů v řadě podle instrukcí.",
            "Ihned slovně zareagujte: „Nevynechávejte otvory!“. Při opakované chybě ihned přerušte pokus," +
            " krátce to probandovi vysvětlete a začněte ho znovu.",
            "Započítejte všechny umístěné kolíky. Výsledky z anulovaného pokusu nepočítejte."
        ),
        new PptRuleRow(
            "Kolík nebyl zcela zastrčen do otvoru, ale během pokusu to proband sám opravil testovanou rukou.",
            "Nechte pokus mlčky běžet dál.",
            "Umístěnou součástku normálně započítejte."
        ),
        new PptRuleRow(
            "Kolík nebyl zcela zastrčen do otvoru a zůstal tak.",
            "Ihned slovně zareagujte: „Zastrčte ho!“.",
            "Nedostatečně zastrčený kolík nepočítejte. Správnou rukou dozastrčený kolík normálně započítejte."
        ),
        new PptRuleRow(
            "Kolík nebyl zcela zastrčen do otvoru, ale během pokusu sám gravitací zapadl do otvoru.",
            "Nechte pokus mlčky běžet dál.",
            "Takový kolík nepočítejte."
        ),
        new PptRuleRow(
            "Řádné umístění součástky dle instrukcí bylo dokončeno těsně po vypršení časového limitu.",
            "Na situaci nijak nereagujte.",
            "Takovou součástku již nepočítejte."
        ),
        new PptRuleRow(
            "Součástka byla omylem vytažena / vyražena z příslušného místa, kam již byla řádně umístěna.",
            "Nechte pokus mlčky běžet dál- na případný dotaz můžete slovně zareagovat: „Pokračujte!“.",
            "Vytaženou / vyraženou součástku nepočítejte (ani kvůli přilepení součástky na prsty). Znovu řádně" +
            " umístěnou součástku ze zásobníku normálně započítejte."
        ),
        new PptRuleRow(
            "Proband přeskočil otvor, ze kterého byl omylem vytažen / vyražen řádně umístěný kolík.",
            "Nechte pokus mlčky běžet dál. Na případný dotaz můžete slovně zareagovat: „Pokračujte!“.",
            "Chybějící kolík nepočítejte. Znovu řádně umístěnou součástku normálně započítejte."
        ),
        new PptRuleRow(
            "Proband později doplnil kolík do prázdného přeskočeného otvoru.",
            "Nechte pokus mlčky běžet dál. Proband smí doplnit kolík (nikoliv u testování obou rukou).",
            "Znovu řádně umístěnou součástku normálně započítejte."
        ),
        new PptRuleRow(
            "Proband manipuluje se součástkou jinou rukou, než má.",
            "Ihned slovně zareagujte: „Druhou rukou!“. Při opakované chybě ihned přerušte pokus, krátce to" +
            " probandovi vysvětlete a začněte ho znovu.",
            "Součástku, se kterou manipuloval špatnou rukou, nepočítejte. Výsledky z anulovaného pokusu" +
            " nepočítejte."
        )
    ];

    public IEnumerable<PptRuleRow> BothHandsRules =>
    [
        new PptRuleRow(
            "Proband nepřemisťuje kolíky současně (nepřibližuje se k otvoru oběma kolíky najednou).",
            "Ihned slovně zareagujte: „Oběma najednou!“. Při dalším opakování této chyby ihned přerušte pokus," +
            " krátce to probandovi vysvětlete a začněte ho znovu.",
            "Nepočítejte páry, které nepřemisťoval současně pravou i levou rukou. Výsledky z anulovaného" +
            " pokusu nepočítejte."
        ),
        new PptRuleRow(
            "Proband dopomohl jednou rukou při manipulaci s kolíkem ruce druhé.",
            "Ihned slovně zareagujte: „Nepomáhejte si!“. Při druhém opakování této chyby ihned přerušte pokus," +
            " krátce to probandovi vysvětlete a začněte ho znovu.",
            "Nepočítejte páry, při jejichž přemístění jednou rukou si dopomáhal druhou rukou. Výsledky z" +
            " anulovaného pokusu nepočítejte."
        ),
        new PptRuleRow(
            "Jeden z páru kolíků nebyl v časovém limitu řádně umístěn ani doplněn do otvoru (chybí).",
            "Nechte pokus mlčky běžet dál. Na případný dotaz můžete slovně zareagovat: „Pokračujte!“.",
            "Nepočítejte pár, ve kterém jeden kolík chybí. Dodatečně doplněný kolík nepočítejte (nebyl umístěn" +
            " zároveň s tím druhým)."
        ),
        new PptRuleRow(
            "Jeden z páru kolíků, který již byl řádně umístěn do otvoru, z něj byl omylem vytažen / vyražen" +
            " (chybí).",
            "Nechte pokus mlčky běžet dál- na případný dotaz můžete slovně zareagovat: „Pokračujte!“.",
            "Pár s vytaženým/vyraženým kolíkem nepočítejte (ani kvůli přilepení kolíku na prsty). Dodatečně" +
            " doplněný pár kolíků nepočítejte (nebyl umístěn zároveň s tím druhým)."
        ),
        new PptRuleRow(
            "Jeden z páru kolíků, který již byl řádně umístěn do otvoru, z něj byl omylem vytažen / vyražen," +
            " ale proband ho později doplnil.",
            "Nechte pokus mlčky běžet dál.",
            "Dodatečně doplněný pár kolíků nepočítejte (nebyl umístěn zároveň s tím druhým)."
        )
    ];

    public IEnumerable<PptRuleRow> AssemblyRules =>
    [
        new PptRuleRow(
            "Proband si omylem shodil již řádně umístěnou součástku z kompletu (nebo i více) a něco tak v" +
            " kompletu či kompletech chybí.",
            "Nechte pokus mlčky běžet dál. Na případný dotaz můžete slovně zareagovat: „Pokračujte!“.",
            "Shozené součástky nepočítejte."
        ),
        new PptRuleRow(
            "Proband si omylem shodil již řádně umístěnou součástku z kompletu (nebo i více), ale proband to" +
            " správnou rukou napravil a součástku/y doplnil.",
            "Nechte pokus mlčky běžet dál. Na případný dotaz můžete slovně zareagovat: „Pokračujte!“.",
            "Znovu umístěnou součástku správnou rukou normálně započítejte. Nepočítejte součástky umístěné" +
            " špatnou rukou."
        ),
        new PptRuleRow(
            "Proband si omylem vyrazil kolík z kompletu a nemá na něj jak umisťovat další součástky.",
            "Ihned slovně zareagujte: „Začněte nový!“.",
            "Započítejte součástky, které byly řádně umístěné, a zůstaly poblíž otvoru (např. podložka a" +
            " trubička)."
        ),
        new PptRuleRow(
            "Proband zaměnil pořadí rukou nebo součástek během kompletování.",
            "Nechte pokus mlčky běžet dál. V případě více než 4 součástek za sebou, se kterými manipuloval" +
            " špatným způsobem, ihned přerušte pokus, krátce to probandovi vysvětlete a začněte ho znovu.",
            "Započítejte jen součástky, které byly umístěné správným způsobem. Nepočítejte součástky umístěné" +
            " špatným způsobem."
        ),
        new PptRuleRow(
            "Proband nedodržel instrukce při manipulaci s více než 4 součástkami ihned za sebou.",
            "Ihned přerušte pokus, krátce to probandovi vysvětlete a začněte ho znovu.",
            "Výsledky z anulovaného pokusu nepočítejte."
        )
    ];
}