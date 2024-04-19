using TestAdministration.Models.Data;
using TestAdministration.Models.TestBuilders.Calculators;

namespace TestAdministration.Models.TestBuilders;

/// <summary>
/// <c>ITestSectionBuilder</c> implementation for Nine Hole Peg Test.
/// </summary>
public class NhptTestSectionBuilder(
    ITestCalculator<NhptTestNormProvider> testCalculator
) : AbstractTestSectionBuilder<NhptTestNormProvider>(testCalculator)
{
    public override TestType Type => TestType.Nhpt;
    public override int SectionCount => 2;
    public override int TrialCount => 4;
}