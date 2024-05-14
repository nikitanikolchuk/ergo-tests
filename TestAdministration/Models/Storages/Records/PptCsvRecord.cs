using CsvHelper.Configuration.Attributes;
using static TestAdministration.Models.Storages.Records.CsvRecordConfiguration;

namespace TestAdministration.Models.Storages.Records;

[Delimiter(Delimiter)]
[CultureInfo(Culture)]
public class PptCsvRecord
{
    public static readonly IList<string> NoteNames =
    [
        "Dom. 1. pokus",
        "Dom. 2. pokus",
        "Dom. 3. pokus",
        "Nedom. 1. pokus",
        "Nedom. 2. pokus",
        "Nedom. 3. pokus",
        "Obě HK 1. pokus",
        "Obě HK 2. pokus",
        "Obě HK 3. pokus",
        string.Empty,
        string.Empty,
        string.Empty,
        "Kompletování 1. pokus",
        "Kompletování 2. pokus",
        "Kompletování 3. pokus",
    ];

    [Index(0)]
    [Name("Testujici")]
    public required string Tester { get; init; }

    [Index(1)]
    [Name("Rodne_cislo")]
    public required string Id { get; init; }

    [Index(2)]
    [Name("Jmeno")]
    public required string Name { get; init; }

    [Index(3)]
    [Name("Prijmeni")]
    public required string Surname { get; init; }

    [Index(4)]
    [Name("Datum")]
    [Format(DateFormat)]
    public required DateOnly Date { get; init; }

    [Index(5)]
    [Name("Cas_zahajeni")]
    [Format(TimeFormat)]
    public required TimeOnly StartTime { get; init; }

    [Index(6)]
    [Name("Cas_ukonceni")]
    [Format(TimeFormat)]
    public required TimeOnly EndTime { get; init; }

    [Index(7)]
    [Name("Dom_1_pokus")]
    [Format(FloatFormat)]
    public required float? DominantFirst { get; init; }

    [Index(8)]
    [Name("Dom_2_pokus")]
    [Format(FloatFormat)]
    public required float? DominantSecond { get; init; }

    [Index(9)]
    [Name("Dom_3_pokus")]
    [Format(FloatFormat)]
    public required float? DominantThird { get; init; }

    [Index(10)]
    [Name("Dom_prumer")]
    [Format(FloatFormat)]
    public required float? DominantAverage { get; init; }

    [Index(11)]
    [Name("Nedom_1_pokus")]
    [Format(FloatFormat)]
    public required float? NonDominantFirst { get; init; }

    [Index(12)]
    [Name("Nedom_2_pokus")]
    [Format(FloatFormat)]
    public required float? NonDominantSecond { get; init; }

    [Index(13)]
    [Name("Nedom_3_pokus")]
    [Format(FloatFormat)]
    public required float? NonDominantThird { get; init; }

    [Index(14)]
    [Name("Nedom_prumer")]
    [Format(FloatFormat)]
    public required float? NonDominantAverage { get; init; }

    [Index(15)]
    [Name("Obe_1_pokus")]
    [Format(FloatFormat)]
    public required float? BothFirst { get; init; }

    [Index(16)]
    [Name("Obe_2_pokus")]
    [Format(FloatFormat)]
    public required float? BothSecond { get; init; }

    [Index(17)]
    [Name("Obe_3_pokus")]
    [Format(FloatFormat)]
    public required float? BothThird { get; init; }

    [Index(18)]
    [Name("Obe_prumer")]
    [Format(FloatFormat)]
    public required float? BothAverage { get; init; }

    [Index(19)]
    [Name("Soucet_1_pokus")]
    [Format(FloatFormat)]
    public required float? TotalFirst { get; init; }

    [Index(20)]
    [Name("Soucet_2_pokus")]
    [Format(FloatFormat)]
    public required float? TotalSecond { get; init; }

    [Index(21)]
    [Name("Soucet_3_pokus")]
    [Format(FloatFormat)]
    public required float? TotalThird { get; init; }

    [Index(22)]
    [Name("Soucet_prumer")]
    [Format(FloatFormat)]
    public required float? TotalAverage { get; init; }

    [Index(23)]
    [Name("Kompletovani_1_pokus")]
    [Format(FloatFormat)]
    public required float? AssemblyFirst { get; init; }

    [Index(24)]
    [Name("Kompletovani_2_pokus")]
    [Format(FloatFormat)]
    public required float? AssemblySecond { get; init; }

    [Index(25)]
    [Name("Kompletovani_3_pokus")]
    [Format(FloatFormat)]
    public required float? AssemblyThird { get; init; }

    [Index(26)]
    [Name("Kompletovani_prumer")]
    [Format(FloatFormat)]
    public required float? AssemblyAverage { get; init; }

    [Index(27)]
    [Name("Dom_1_pokus_SDS")]
    [Format(FloatFormat)]
    public required float? DominantFirstSdScore { get; init; }

    [Index(28)]
    [Name("Dom_2_pokus_SDS")]
    [Format(FloatFormat)]
    public required float? DominantSecondSdScore { get; init; }

    [Index(29)]
    [Name("Dom_3_pokus_SDS")]
    [Format(FloatFormat)]
    public required float? DominantThirdSdScore { get; init; }

    [Index(30)]
    [Name("Dom_prumer_SDS")]
    [Format(FloatFormat)]
    public required float? DominantAverageSdScore { get; init; }

    [Index(31)]
    [Name("Nedom_1_pokus_SDS")]
    [Format(FloatFormat)]
    public required float? NonDominantFirstSdScore { get; init; }

    [Index(32)]
    [Name("Nedom_2_pokus_SDS")]
    [Format(FloatFormat)]
    public required float? NonDominantSecondSdScore { get; init; }

    [Index(33)]
    [Name("Nedom_3_pokus_SDS")]
    [Format(FloatFormat)]
    public required float? NonDominantThirdSdScore { get; init; }

    [Index(34)]
    [Name("Nedom_prumer_SDS")]
    [Format(FloatFormat)]
    public required float? NonDominantAverageSdScore { get; init; }

    [Index(35)]
    [Name("Bbe_1_pokus_SDS")]
    [Format(FloatFormat)]
    public required float? BothFirstSdScore { get; init; }

    [Index(36)]
    [Name("Bbe_2_pokus_SDS")]
    [Format(FloatFormat)]
    public required float? BothSecondSdScore { get; init; }

    [Index(37)]
    [Name("Bbe_3_pokus_SDS")]
    [Format(FloatFormat)]
    public required float? BothThirdSdScore { get; init; }

    [Index(38)]
    [Name("Bbe_prumer_SDS")]
    [Format(FloatFormat)]
    public required float? BothAverageSdScore { get; init; }

    [Index(39)]
    [Name("Soucet_1_pokus_SDS")]
    [Format(FloatFormat)]
    public required float? TotalFirstSdScore { get; init; }

    [Index(40)]
    [Name("Soucet_2_pokus_SDS")]
    [Format(FloatFormat)]
    public required float? TotalSecondSdScore { get; init; }

    [Index(41)]
    [Name("Soucet_3_pokus_SDS")]
    [Format(FloatFormat)]
    public required float? TotalThirdSdScore { get; init; }

    [Index(42)]
    [Name("Soucet_prumer_SDS")]
    [Format(FloatFormat)]
    public required float? TotalAverageSdScore { get; init; }

    [Index(43)]
    [Name("Kompletovani_1_pokus_SDS")]
    [Format(FloatFormat)]
    public required float? AssemblyFirstSdScore { get; init; }

    [Index(44)]
    [Name("Kompletovani_2_pokus_SDS")]
    [Format(FloatFormat)]
    public required float? AssemblySecondSdScore { get; init; }

    [Index(45)]
    [Name("Kompletovani_3_pokus_SDS")]
    [Format(FloatFormat)]
    public required float? AssemblyThirdSdScore { get; init; }

    [Index(46)]
    [Name("Kompletovani_prumer_SDS")]
    [Format(FloatFormat)]
    public required float? AssemblyAverageSdScore { get; init; }

    [Index(47)]
    [Name("Dom_1_pokus_porovnani")]
    public required string DominantFirstNormInterpretation { get; init; }

    [Index(48)]
    [Name("Dom_2_pokus_porovnani")]
    public required string DominantSecondNormInterpretation { get; init; }

    [Index(49)]
    [Name("Dom_3_pokus_porovnani")]
    public required string DominantThirdNormInterpretation { get; init; }

    [Index(50)]
    [Name("Dom_prumer_porovnani")]
    public required string DominantAverageNormInterpretation { get; init; }

    [Index(51)]
    [Name("Nedom_1_pokus_porovnani")]
    public required string NonDominantFirstNormInterpretation { get; init; }

    [Index(52)]
    [Name("Nedom_2_pokus_porovnani")]
    public required string NonDominantSecondNormInterpretation { get; init; }

    [Index(53)]
    [Name("Nedom_3_pokus_porovnani")]
    public required string NonDominantThirdNormInterpretation { get; init; }

    [Index(54)]
    [Name("Nedom_prumer_porovnani")]
    public required string NonDominantAverageNormInterpretation { get; init; }

    [Index(55)]
    [Name("Bbe_1_pokus_porovnani")]
    public required string BothFirstNormInterpretation { get; init; }

    [Index(56)]
    [Name("Bbe_2_pokus_porovnani")]
    public required string BothSecondNormInterpretation { get; init; }

    [Index(57)]
    [Name("Bbe_3_pokus_porovnani")]
    public required string BothThirdNormInterpretation { get; init; }

    [Index(58)]
    [Name("Bbe_prumer_porovnani")]
    public required string BothAverageNormInterpretation { get; init; }

    [Index(59)]
    [Name("Soucet_1_pokus_porovnani")]
    public required string TotalFirstNormInterpretation { get; init; }

    [Index(60)]
    [Name("Soucet_2_pokus_porovnani")]
    public required string TotalSecondNormInterpretation { get; init; }

    [Index(61)]
    [Name("Soucet_3_pokus_porovnani")]
    public required string TotalThirdNormInterpretation { get; init; }

    [Index(62)]
    [Name("Soucet_prumer_porovnani")]
    public required string TotalAverageNormInterpretation { get; init; }

    [Index(63)]
    [Name("Kompletovani_1_pokus_porovnani")]
    public required string AssemblyFirstNormInterpretation { get; init; }

    [Index(64)]
    [Name("Kompletovani_2_pokus_porovnani")]
    public required string AssemblySecondNormInterpretation { get; init; }

    [Index(65)]
    [Name("Kompletovani_3_pokus_porovnani")]
    public required string AssemblyThirdNormInterpretation { get; init; }

    [Index(66)]
    [Name("Kompletovani_prumer_porovnani")]
    public required string AssemblyAverageNormInterpretation { get; init; }

    [Index(67)]
    [Name("Poznamky")]
    public required string Notes { get; init; }
}