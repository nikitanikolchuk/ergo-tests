namespace TestAdministration.Models.Data;

/// <summary>
/// An immutable class for representing data needed for patient
/// identification without diacritics used in directory names.
/// </summary>
/// <param name="Id">Birth certificate number or its equivalent.</param>
/// <param name="Name">Patient's name.</param>
/// <param name="Surname">Patient's surname.</param>
public record PatientDirectoryInfo(
    string Id,
    string Name,
    string Surname
);