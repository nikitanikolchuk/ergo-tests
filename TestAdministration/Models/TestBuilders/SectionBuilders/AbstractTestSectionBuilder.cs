using System.Collections.Immutable;
using TestAdministration.Models.Data;
using TestAdministration.Models.TestBuilders.SectionBuilders.Calculators;

namespace TestAdministration.Models.TestBuilders.SectionBuilders;

/// <summary>
/// Base implementation of <see cref="ITestSectionBuilder"/> interface.
/// </summary>
/// <param name="testCalculator">A calculator for norm comparisons.</param>
public abstract class AbstractTestSectionBuilder<T>(
    ITestCalculator<T> testCalculator
) : ITestSectionBuilder where T : ITestNormProvider
{
    public abstract TestType Type { get; }
    public abstract int SectionCount { get; }
    public abstract bool HasPracticeTrial { get; }

    public TestTrial BuildTrial(float? value, string note, int section, Patient patient)
    {
        var sdScore = value != null
            ? testCalculator.SdScore(value.Value, section, patient)
            : null;

        return new TestTrial(value, sdScore, note);
    }

    public virtual ImmutableList<TestSection> BuildSections(List<List<TestTrial>> trials, Patient patient) =>
        trials.Select(trialList =>
            {
                var values = trialList.Select(t => t.Value);
                var sdScores = trialList.Select(t => t.SdScore);

                return new TestSection(
                    _nullableAverage(values),
                    _nullableAverage(sdScores),
                    trialList.ToImmutableList()
                );
            }
        ).ToImmutableList();

    private float? _nullableAverage(IEnumerable<float?> values)
    {
        var nonNullValues = values
            .Skip(HasPracticeTrial ? 1 : 0)
            .OfType<float>()
            .ToList();

        return nonNullValues.Count > 0 ? nonNullValues.Average() : null;
    }
}