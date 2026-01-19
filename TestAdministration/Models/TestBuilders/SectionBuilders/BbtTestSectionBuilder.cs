using TestAdministration.Models.Data;
using TestAdministration.Models.TestBuilders.SectionBuilders.Calculators;

namespace TestAdministration.Models.TestBuilders.SectionBuilders;

/// <summary>
/// <c>ITestSectionBuilder</c> implementation for Box and Block Test.
/// </summary>
public class BbtTestSectionBuilder(
    ITestCalculator<BbtTestNormProvider> testCalculator
) : AbstractTestSectionBuilder<BbtTestNormProvider>(testCalculator)
{
    public override TestType Type => TestType.Bbt;
    public override int SectionCount => 2;
    public override bool HasPracticeTrial => true;
}