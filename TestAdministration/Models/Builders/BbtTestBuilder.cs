using TestAdministration.Models.Calculators;
using TestAdministration.Models.Data;

namespace TestAdministration.Models.Builders;

/// <summary>
/// <c>ITestBuilder</c> implementation for Box and Block Test.
/// </summary>
public class BbtTestBuilder(
    TestCalculator testCalculator,
    Patient patient
) : AbstractTestBuilder(testCalculator, patient)
{
    protected override TestType Type => TestType.Bbt;
    protected override int SectionCount => 2;
    protected override int TrialCount => 4;
}