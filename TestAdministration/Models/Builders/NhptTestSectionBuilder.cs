using TestAdministration.Models.Calculators;
using TestAdministration.Models.Data;

namespace TestAdministration.Models.Builders;

/// <summary>
/// <c>ITestSectionBuilder</c> implementation for Nine Hole Peg Test.
/// </summary>
public class NhptTestSectionBuilder(
    TestCalculator testCalculator,
    Patient patient
) : AbstractTestSectionBuilder(testCalculator, patient)
{
    public override TestType Type => TestType.Nhpt;
    public override int SectionCount => 2;
    public override int TrialCount => 4;
}