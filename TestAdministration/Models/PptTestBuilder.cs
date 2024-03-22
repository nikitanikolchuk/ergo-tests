using System.Collections.Immutable;

namespace TestAdministration.Models;

/// <summary>
/// <c>ITestBuilder</c> implementation for Purdue Pegboard Test.
/// Additionally creates a test section that contains sums of
/// values from first 3 sections.
/// </summary>
public class PptTestBuilder : AbstractTestBuilder
{
    protected override int SectionCount => 4;
    protected override int TrialCount => 3;

    protected override ImmutableList<TestSection> BuildSections(List<List<TestTrial>> trials)
    {
        var sumTrials = trials.Take(SectionCount - 1).Aggregate(_trialSum);
        var totalTrials = new List<List<TestTrial>>(trials);
        totalTrials.Insert(SectionCount - 1, sumTrials);

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
            trials[i] = new TestTrial(sum, null);
        }

        return trials;
    }
}