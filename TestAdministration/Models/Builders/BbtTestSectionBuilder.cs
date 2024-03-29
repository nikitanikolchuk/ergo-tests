using TestAdministration.Models.Calculators;
using TestAdministration.Models.Data;

namespace TestAdministration.Models.Builders;

/// <summary>
/// <c>ITestSectionBuilder</c> implementation for Box and Block Test.
/// </summary>
public class BbtTestSectionBuilder(
    ITestCalculator<BbtTestNormProvider> testCalculator
) : AbstractTestSectionBuilder<BbtTestNormProvider>(testCalculator)
{
    public override TestType Type => TestType.Bbt;
    public override int SectionCount => 2;
    public override int TrialCount => 4;
}