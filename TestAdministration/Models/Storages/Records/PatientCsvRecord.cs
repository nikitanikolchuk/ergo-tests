using CsvHelper.Configuration.Attributes;
using TestAdministration.Models.Data;
using static TestAdministration.Models.Storages.Records.CsvRecordConfiguration;

namespace TestAdministration.Models.Storages.Records;

[Delimiter(Delimiter)]
[CultureInfo(Culture)]
public class PatientCsvRecord
{
    [Index(0)]
    [Name("Rodne_cislo")]
    public required string Id { get; init; }

    [Index(1)]
    [Name("Jmeno")]
    public required string Name { get; init; }

    [Index(2)]
    [Name("Prijmeni")]
    public required string Surname { get; init; }

    [Index(3)]
    [Name("Pohlavi")]
    [BooleanTrueValues("m")]
    [BooleanFalseValues("ž")]
    public required bool IsMale { get; init; }

    [Index(4)]
    [Name("Datum_narozeni")]
    [Format(DateFormat)]
    public required DateOnly BirthDate { get; init; }

    [Index(5)]
    [Name("Dominantni_HK")]
    [TypeConverter(typeof(HandCsvStringConverter))]
    public required Hand DominantHand { get; init; }

    [Index(6)]
    [Name("HK_s_patologii")]
    [TypeConverter(typeof(HandCsvStringConverter))]
    public required Hand PathologicalHand { get; init; }
}