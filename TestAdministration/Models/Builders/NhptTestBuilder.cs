using TestAdministration.Models.Calculators;

namespace TestAdministration.Models.Builders;

/// <summary>
/// <c>ITestBuilder</c> implementation for Nine Hole Peg Test.
/// </summary>
public class NhptTestBuilder(
    TestCalculator testCalculator,
    Patient patient
) : AbstractTestBuilder(testCalculator, patient)
{
    protected override int SectionCount => 2;
    protected override int TrialCount => 4;
}