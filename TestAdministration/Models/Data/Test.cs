using System.Collections.Immutable;

namespace TestAdministration.Models.Data;

/// <summary>
/// An immutable class representing a single test entry.
/// </summary>
/// <param name="Tester">Tester's name.</param>
/// <param name="Date">Testing date.</param>
/// <param name="StartTime">Test's start time.</param>
/// <param name="EndTime">Test's end time.</param>
/// <param name="Sections">An immutable list of test sections.</param>
public record Test(
    TestType Type,
    string Tester,
    DateOnly Date,
    TimeOnly StartTime,
    TimeOnly EndTime,
    ImmutableList<TestSection> Sections
)
{
    public virtual bool Equals(Test? other)
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

        return Tester == other.Tester &&
               Date == other.Date &&
               StartTime == other.StartTime &&
               EndTime == other.EndTime &&
               Sections.SequenceEqual(other.Sections);
    }

    public override int GetHashCode() =>
        HashCode.Combine(Tester, Date, StartTime, EndTime, Sections);
}