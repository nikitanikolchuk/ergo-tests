using System.ComponentModel;
using TestAdministration.Models.Data;
using static TestAdministration.Models.Mappers.CsvMapperConfiguration;

namespace TestAdministration.Models.Mappers;

/// <summary>
/// A static class for mapping <c>Patient</c> objects to CSV records.
/// </summary>
public static class PatientCsvMapper
{
    /// <summary>
    /// Maps <c>Patient</c> object to a CSV record and converts
    /// all values to strings.
    /// </summary>
    /// <param name="patient">The patient data to convert.</param>
    /// <returns>A CSV record containing the patient's data.</returns>
    public static PatientCsvRecord ToCsvRecord(Patient patient) => new(
        patient.Id,
        patient.IsMale ? "m" : "ž",
        patient.BirthDate.ToString(DateFormat),
        _handString(patient.DominantHand),
        _handString(patient.PathologicalHand)
    );

    private static string _handString(Hand hand) => hand switch
    {
        Hand.Left => "levá",
        Hand.Right => "pravá",
        Hand.Both => "obě",
        _ => throw new InvalidEnumArgumentException(
            nameof(hand),
            Convert.ToInt32(hand),
            typeof(Hand)
        )
    };
}