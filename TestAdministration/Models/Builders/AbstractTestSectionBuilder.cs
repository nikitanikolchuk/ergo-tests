using System.Collections.Immutable;
using TestAdministration.Models.Calculators;
using TestAdministration.Models.Data;

namespace TestAdministration.Models.Builders;

/// <summary>
/// Base implementation of <c>ITestSectionBuilder</c> interface.
/// </summary>
/// <param name="testCalculator">A calculator for norm comparisons.</param>
public abstract class AbstractTestSectionBuilder<T>(
    ITestCalculator<T> testCalculator
) : ITestSectionBuilder where T : ITestNormProvider
{
    public abstract TestType Type { get; }
    public abstract int SectionCount { get; }
    public abstract int TrialCount { get; }

    public TestTrial BuildTrial(float? value, string? note, int section, Patient patient)
    {
        float? sdScore = value != null
            ? testCalculator.SdScore(value.Value, section, patient)
            : null;
        float? normDifference = value != null
            ? testCalculator.NormDifference(value.Value, section, patient)
            : null;
        return new TestTrial(value, sdScore, normDifference, note);
    }

    public virtual ImmutableList<TestSection> BuildSections(List<List<TestTrial>> trials, Patient patient) =>
        trials.Select(trialList =>
            {
                var values = trialList.Select(t => t.Value);
                var sdScores = trialList.Select(t => t.SdScore);
                var normDifferences = trialList.Select(t => t.NormDifference);

                return new TestSection(
                    _nullableAverage(values),
                    _nullableAverage(sdScores),
                    _nullableAverage(normDifferences),
                    trialList.ToImmutableList()
                );
            }
        ).ToImmutableList();

    private static float? _nullableAverage(IEnumerable<float?> values)
    {
        var nonNullValues = values.Where(v => v != null).ToList();
        return nonNullValues.Count > 0 ? nonNullValues.Average() : null;
    }
}