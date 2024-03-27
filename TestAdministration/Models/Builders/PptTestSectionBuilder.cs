using System.Collections.Immutable;
using TestAdministration.Models.Calculators;
using TestAdministration.Models.Data;

namespace TestAdministration.Models.Builders;

/// <summary>
/// <c>ITestSectionBuilder</c> implementation for Purdue Pegboard Test.
/// Additionally creates a test section that contains sums of
/// values from first 3 sections.
/// </summary>
public class PptTestSectionBuilder(
    TestCalculator testCalculator,
    Patient patient
) : AbstractTestSectionBuilder(testCalculator, patient)
{
    private const int TotalSection = 3;

    public override TestType Type => TestType.Ppt;
    public override int SectionCount => 4;
    public override int TrialCount => 3;

    public override ImmutableList<TestSection> BuildSections(List<List<TestTrial>> trials)
    {
        var sumTrials = trials.Take(SectionCount - 1).Aggregate(_trialSum);
        var totalTrials = new List<List<TestTrial>>(trials);
        totalTrials.Insert(TotalSection, sumTrials);

        return base.BuildSections(totalTrials);
    }

    private List<TestTrial> _trialSum(List<TestTrial> first, List<TestTrial> second)
    {
        if (first.Count != TrialCount || second.Count != TrialCount)
        {
            throw new ArgumentException("Invalid number of trials in a PPT section");
        }

        var trials = new List<TestTrial>(first);
        for (var i = 0; i < TrialCount; i++)
        {
            if (!second[i].Value.HasValue)
            {
                continue;
            }

            var sum = trials[i].Value + second[i].Value ?? second[i].Value;
            trials[i] = BuildTrial(sum, null, TotalSection);
        }

        return trials;
    }
}