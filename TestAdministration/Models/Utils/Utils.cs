namespace TestAdministration.Models.Utils;

/// <summary>
/// A class for static helper functions.
/// </summary>
public static class Utils
{
    /// <summary>
    /// Check if two nullable <c>float</c> values are equal
    /// using minimal difference tolerance.
    /// </summary>
    public static bool NearlyEqual(float? x, float? y)
    {
        if (x == null & y == null)
        {
            return true;
        }

        if (x == null || y == null)
        {
            return false;
        }

        const float epsilon = 1E-6f;
        var tolerance = Math.Max(Math.Abs(x.Value), Math.Abs(y.Value)) * epsilon;

        return x.Equals(y) || Math.Abs(x.Value - y.Value) <= tolerance;
    }
}