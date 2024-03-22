namespace TestAdministration.Models.Calculators;

/// <summary>
/// Base implementation of <c>ITestCalculator</c> interface.
/// </summary>
public abstract class AbstractTestCalculator : ITestCalculator
{
    public float SdScore(float value, int section, bool isMale, int age, bool isRightDominant)
    {
        var norm = GetNorm(section, isMale, age, isRightDominant);
        return (value - norm.Average) / norm.Sd;
    }

    public float NormDifference(float value, int section, bool isMale, int age, bool isRightDominant)
    {
        var norm = GetNorm(section, isMale, age, isRightDominant);
        return value - norm.Average;
    }

    /// <summary>
    /// Get corresponding norm values.
    /// </summary>
    /// <param name="section">Test's section number starting from zero.</param>
    /// <param name="isMale">Is true if the patient is male.</param>
    /// <param name="age">The patient's age.</param>
    /// <param name="isRightDominant">Is true if the patient's dominant hand is right.</param>
    /// <returns>A corresponding <c>TestNorm</c>.</returns>
    protected abstract TestNorm GetNorm(int section, bool isMale, int age, bool isRightDominant);
}