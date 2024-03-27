using CsvHelper.Configuration.Attributes;

namespace TestAdministration.Models.Mappers;

/// <summary>
/// An immutable class that defines CSV format for
/// <c>Patient</c> objects.
/// </summary>
/// <param name="Id">Birth certificate number or its equivalent.</param>
/// <param name="Sex">Gender.</param>
/// <param name="BirthDate">Birth date.</param>
/// <param name="DominantHand">The hand used for writing.</param>
/// <param name="PathologicalHand">Hand(s) with a pathology.</param>
public record PatientCsvRecord(
    [property: Index(0), Name("Rodne_cislo")]
    string Id,
    [property: Index(1), Name("Pohlavi")]
    string Sex,
    [property: Index(2), Name("Datum_narozeni")]
    string BirthDate,
    [property: Index(3), Name("Dominantni_HK")]
    string DominantHand,
    [property: Index(4), Name("HK_s_patologii")]
    string PathologicalHand
);