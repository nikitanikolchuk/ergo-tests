using System.Collections.Immutable;

namespace TestAdministration.Models;

/// <summary>
/// An immutable class representing a single test section.
/// </summary>
/// <param name="Trials">An immutable list of test values.</param>
public record TestSection(
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

        return GetType() == other.GetType() && 
               Trials.SequenceEqual(other.Trials);
    }

    public override int GetHashCode() => Trials.GetHashCode();
}