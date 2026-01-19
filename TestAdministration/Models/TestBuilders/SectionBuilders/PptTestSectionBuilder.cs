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
    public override bool HasPracticeTrial => false;

    public override ImmutableList<TestSection> BuildSections(List<List<TestTrial>> trials, Patient patient)
    {
        var sumTrials = trials
            .Take(SectionCount - 1)
            .Select(l => l.Select(t => t.Value).ToList())
            .Aggregate(_valueSum)
            .Select(value => BuildTrial(value, string.Empty, -1, patient))
            .ToList();
        var totalTrials = new List<List<TestTrial>>(trials);
        totalTrials.Insert(TotalSection, sumTrials);

        return base.BuildSections(totalTrials, patient);
    }

    private static List<float?> _valueSum(List<float?> first, List<float?> second)
    {
        if (first.Count != second.Count)
        {
            throw new ArgumentException("Invalid number of trials in a PPT section");
        }

        var totals = new List<float?>(first);
        for (var i = 0; i < first.Count; i++)
        {
            if (second[i] is null)
            {
                continue;
            }

            totals[i] = totals[i] + second[i] ?? second[i];
        }

        return totals;
    }
}