using System.Collections.Immutable;

namespace TestAdministration.Models;

/// <summary>
/// An immutable class representing a single test section.
/// </summary>
/// <param name="AverageValue">Average of trial values.</param>
/// <param name="AverageSdScore">Average of trial SD scores.</param>
/// <param name="AverageNormDifference">Average of trial norm differences.</param>
/// <param name="Trials">An immutable list of test values.</param>
public record TestSection(
    float? AverageValue,
    float? AverageSdScore,
    float? AverageNormDifference,
    ImmutableList<TestTrial> Trials
)
{
    public virtual bool Equals(TestSection? other)
    {
        if (other == null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        if (GetType() != other.GetType())
        {
            return false;
        }

        return Utils.Utils.NearlyEqual(AverageValue, other.AverageValue) &&
               Utils.Utils.NearlyEqual(AverageSdScore, other.AverageSdScore) &&
               Utils.Utils.NearlyEqual(AverageNormDifference, other.AverageNormDifference) &&
               Trials.SequenceEqual(other.Trials);
    }

    public override int GetHashCode() => Trials.GetHashCode();
}