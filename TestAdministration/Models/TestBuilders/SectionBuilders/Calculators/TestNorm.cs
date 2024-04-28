namespace TestAdministration.Models.TestBuilders.SectionBuilders.Calculators;

/// <summary>
/// An immutable class representing a norm for a particular test section.
/// </summary>
/// <param name="Sd">Standard Deviation.</param>
/// <param name="Average">Average value.</param>
public record TestNorm(
    float Sd,
    float Average
);