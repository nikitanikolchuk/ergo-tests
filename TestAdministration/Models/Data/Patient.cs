namespace TestAdministration.Models.Data;

/// <summary>
/// An immutable class representing a Patient.
/// </summary>
/// <param name="Id">Birth certificate number or its equivalent.</param>
/// <param name="IsMale">Gender.</param>
/// <param name="BirthDate">Birth date.</param>
/// <param name="DominantHand">The hand used for writing.</param>
/// <param name="PathologicalHand">Hand(s) with a pathology.</param>
public record Patient(
    string Id,
    bool IsMale,
    DateOnly BirthDate,
    Hand DominantHand,
    Hand PathologicalHand
);