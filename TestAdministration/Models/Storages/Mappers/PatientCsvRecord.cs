using CsvHelper.Configuration.Attributes;
using TestAdministration.Models.Data;
using static TestAdministration.Models.Storages.Mappers.CsvMapperConfiguration;

namespace TestAdministration.Models.Storages.Mappers;

[Delimiter(Delimiter)]
[CultureInfo(Culture)]
public class PatientCsvRecord
{
    [Index(0)]
    [Name("Rodne_cislo")]
    public string? Id { get; init; }

    [Index(1)]
    [Name("Jmeno")]
    public string? Name { get; init; }

    [Index(2)]
    [Name("Prijmeni")]
    public string? Surname { get; init; }

    [Index(3)]
    [Name("Pohlavi")]
    [BooleanTrueValues("m")]
    [BooleanFalseValues("ž")]
    public bool? IsMale { get; init; }

    [Index(4)]
    [Name("Datum_narozeni")]
    [Format(DateFormat)]
    public DateOnly? BirthDate { get; init; }

    [Index(5)]
    [Name("Dominantni_HK")]
    [TypeConverter(typeof(HandCsvConverter))]
    public Hand? DominantHand { get; init; }

    [Index(6)]
    [Name("HK_s_patologii")]
    [TypeConverter(typeof(HandCsvConverter))]
    public Hand? PathologicalHand { get; init; }
}