namespace TestAdministration.Models.Data;

/// <summary>
/// An immutable class for storing a single test value with an
/// attached note.
/// </summary>
/// <param name="Value">Test value.</param>
/// <param name="SdScore">Standard Deviation Score.</param>
/// <param name="NormDifference">
/// Difference between this value and average norm value.
/// </param>
/// <param name="Note">Attached note.</param>
public record TestTrial(
    float? Value,
    float? SdScore,
    float? NormDifference,
    string? Note
)
{
    public virtual bool Equals(TestTrial? other)
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

        return Utils.Utils.NearlyEqual(Value, other.Value) &&
               Utils.Utils.NearlyEqual(SdScore, other.SdScore) &&
               Utils.Utils.NearlyEqual(NormDifference, other.NormDifference) &&
               Note == other.Note;
    }

    public override int GetHashCode() =>
        HashCode.Combine(Value, SdScore, NormDifference, Note);
}