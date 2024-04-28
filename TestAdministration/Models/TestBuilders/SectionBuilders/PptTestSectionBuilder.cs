using System.Collections.Immutable;
using TestAdministration.Models.Data;
using TestAdministration.Models.TestBuilders.SectionBuilders.Calculators;

namespace TestAdministration.Models.TestBuilders.SectionBuilders;

/// <summary>
/// <see cref="ITestSectionBuilder"/> implementation for Purdue Pegboard Test.
/// Additionally, creates a test section that contains sums of
/// values from first 3 sections.
/// </summary>
public class PptTestSectionBuilder(
    ITestCalculator<PptTestNormProvider> testCalculator
) : AbstractTestSectionBuilder<PptTestNormProvider>(testCalculator)
{
    private const int TotalSection = 3;

    public override TestType Type => TestType.Ppt;
    public override int SectionCount => 4;
    public override int TrialCount => 3;

    public override ImmutableList<TestSection> BuildSections(List<List<TestTrial>> trials, Patient patient)
    {
        var sumTrials = trials
            .Take(SectionCount - 1)
            .Aggregate((l1, l2) => _trialSum(l1, l2, patient));
        var totalTrials = new List<List<TestTrial>>(trials);
        totalTrials.Insert(TotalSection, sumTrials);

        return base.BuildSections(totalTrials, patient);
    }

    private List<TestTrial> _trialSum(List<TestTrial> first, List<TestTrial> second, Patient patient)
    {
        if (first.Count != TrialCount || second.Count != TrialCount)
        {
            throw new ArgumentException("Invalid number of trials in a PPT section");
        }

        var trials = new List<TestTrial>(first);
        for (var i = 0; i < TrialCount; i++)
        {
            if (second[i].Value == null)
            {
                continue;
            }

            var sum = trials[i].Value + second[i].Value ?? second[i].Value;
            trials[i] = BuildTrial(sum, string.Empty, TotalSection, patient);
        }

        return trials;
    }
}