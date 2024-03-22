namespace TestAdministration.Models.Calculators;

/// <summary>
/// An interface for calculating comparison values.
/// </summary>
public interface ITestCalculator
{
    /// <summary>
    /// Calculates Standard Deviation Score for a test value.
    /// </summary>
    /// <param name="value">The measured test value.</param>
    /// <param name="section">Test's section number starting from zero.</param>
    /// <param name="isMale">Is true if the patient is male.</param>
    /// <param name="age">The patient's age.</param>
    /// <param name="isRightDominant">Is true if the patient's dominant hand is right.</param>
    /// <returns>The calculated Standard Deviation Score.</returns>
    float SdScore(float value, int section, bool isMale, int age, bool isRightDominant);

    /// <summary>
    /// Calculates difference between a measured value and
    /// corresponding average value.
    /// </summary>
    /// <param name="value">The measured test value.</param>
    /// <param name="section">Test's section number starting from zero.</param>
    /// <param name="isMale">Is true if the patient is male.</param>
    /// <param name="age">The patient's age.</param>
    /// <param name="isRightDominant">Is true if the patient's dominant hand is right.</param>
    /// <returns>The calculated difference between values.</returns>
    float NormDifference(float value, int section, bool isMale, int age, bool isRightDominant);
}