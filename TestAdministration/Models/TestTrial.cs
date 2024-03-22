namespace TestAdministration.Models;

/// <summary>
/// An immutable class for storing a single test value with an attached note.
/// </summary>
/// <param name="Value">Test value.</param>
/// <param name="Note">Attached note.</param>
public record TestTrial(
    float? Value,
    string? Note
);