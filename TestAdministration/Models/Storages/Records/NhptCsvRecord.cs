using CsvHelper.Configuration.Attributes;
using static TestAdministration.Models.Storages.Records.CsvRecordConfiguration;

namespace TestAdministration.Models.Storages.Records;

[Delimiter(Delimiter)]
[CultureInfo(Culture)]
public class NhptCsvRecord
{
    public static readonly IList<string> NoteNames =
    [
        "Dom. zkuš. pokus",
        "Dom. 1. pokus",
        "Dom. 2. pokus",
        "Dom. 3. pokus",
        "Nedom. zkuš. pokus",
        "Nedom. 1. pokus",
        "Nedom. 2. pokus",
        "Nedom. 3. pokus"
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
    [Name("Dom_zkus")]
    [Format(FloatFormat)]
    public required float? DominantPractice { get; init; }

    [Index(8)]
    [Name("Dom_1_pokus")]
    [Format(FloatFormat)]
    public required float? DominantFirst { get; init; }

    [Index(9)]
    [Name("Dom_2_pokus")]
    [Format(FloatFormat)]
    public required float? DominantSecond { get; init; }

    [Index(10)]
    [Name("Dom_3_pokus")]
    [Format(FloatFormat)]
    public required float? DominantThird { get; init; }

    [Index(11)]
    [Name("Dom_prumer")]
    [Format(FloatFormat)]
    public required float? DominantAverage { get; init; }

    [Index(12)]
    [Name("Nedom_zkus")]
    [Format(FloatFormat)]
    public required float? NonDominantPractice { get; init; }

    [Index(13)]
    [Name("Nedom_1_pokus")]
    [Format(FloatFormat)]
    public required float? NonDominantFirst { get; init; }

    [Index(14)]
    [Name("Nedom_2_pokus")]
    [Format(FloatFormat)]
    public required float? NonDominantSecond { get; init; }

    [Index(15)]
    [Name("Nedom_3_pokus")]
    [Format(FloatFormat)]
    public required float? NonDominantThird { get; init; }

    [Index(16)]
    [Name("Nedom_prumer")]
    [Format(FloatFormat)]
    public required float? NonDominantAverage { get; init; }

    [Index(17)]
    [Name("Dom_zkus_SDS")]
    [Format(FloatFormat)]
    public required float? DominantPracticeSdScore { get; init; }

    [Index(18)]
    [Name("Dom_1_pokus_SDS")]
    [Format(FloatFormat)]
    public required float? DominantFirstSdScore { get; init; }

    [Index(19)]
    [Name("Dom_2_pokus_SDS")]
    [Format(FloatFormat)]
    public required float? DominantSecondSdScore { get; init; }

    [Index(20)]
    [Name("Dom_3_pokus_SDS")]
    [Format(FloatFormat)]
    public required float? DominantThirdSdScore { get; init; }

    [Index(21)]
    [Name("Dom_prumer_SDS")]
    [Format(FloatFormat)]
    public required float? DominantAverageSdScore { get; init; }

    [Index(22)]
    [Name("Nedom_zkus_SDS")]
    [Format(FloatFormat)]
    public required float? NonDominantPracticeSdScore { get; init; }

    [Index(23)]
    [Name("Nedom_1_pokus_SDS")]
    [Format(FloatFormat)]
    public required float? NonDominantFirstSdScore { get; init; }

    [Index(24)]
    [Name("Nedom_2_pokus_SDS")]
    [Format(FloatFormat)]
    public required float? NonDominantSecondSdScore { get; init; }

    [Index(25)]
    [Name("Nedom_3_pokus_SDS")]
    [Format(FloatFormat)]
    public required float? NonDominantThirdSdScore { get; init; }

    [Index(26)]
    [Name("Nedom_prumer_SDS")]
    [Format(FloatFormat)]
    public required float? NonDominantAverageSdScore { get; init; }

    [Index(27)]
    [Name("Dom_zkus_porovnani")]
    public required string DominantPracticeNormInterpretation { get; init; }

    [Index(28)]
    [Name("Dom_1_pokus_porovnani")]
    public required string DominantFirstNormInterpretation { get; init; }

    [Index(29)]
    [Name("Dom_2_pokus_porovnani")]
    public required string DominantSecondNormInterpretation { get; init; }

    [Index(30)]
    [Name("Dom_3_pokus_porovnani")]
    public required string DominantThirdNormInterpretation { get; init; }

    [Index(31)]
    [Name("Dom_prumer_porovnani")]
    public required string DominantAverageNormInterpretation { get; init; }

    [Index(32)]
    [Name("Nedom_zkus_porovnani")]
    public required string NonDominantPracticeNormInterpretation { get; init; }

    [Index(33)]
    [Name("Nedom_1_pokus_porovnani")]
    public required string NonDominantFirstNormInterpretation { get; init; }

    [Index(34)]
    [Name("Nedom_2_pokus_porovnani")]
    public required string NonDominantSecondNormInterpretation { get; init; }

    [Index(35)]
    [Name("Nedom_3_pokus_porovnani")]
    public required string NonDominantThirdNormInterpretation { get; init; }

    [Index(36)]
    [Name("Nedom_prumer_porovnani")]
    public required string NonDominantAverageNormInterpretation { get; init; }

    [Index(37)]
    [Name("Poznamky")]
    public required string Notes { get; init; }
}