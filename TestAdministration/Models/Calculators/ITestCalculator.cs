using TestAdministration.Models.Data;

namespace TestAdministration.Models.Calculators;

/// <summary>
/// An interface for calculating values used in tests.
/// </summary>
// ReSharper disable once UnusedTypeParameter
public interface ITestCalculator<T> where T : ITestNormProvider
{
    /// <summary>
    /// Calculates Standard Deviation Score for a test value.
    /// </summary>
    /// <param name="value">The measured test value.</param>
    /// <param name="section">Test's section number starting from zero.</param>
    /// <param name="patient">The tested patient.</param>
    /// <returns>The calculated Standard Deviation Score.</returns>
    public float SdScore(float value, int section, Patient patient);

    /// <summary>
    /// Calculates difference between a measured value and
    /// corresponding average value.
    /// </summary>
    /// <param name="value">The measured test value.</param>
    /// <param name="section">Test's section number starting from zero.</param>
    /// <param name="patient">The tested patient.</param>
    /// <returns>The calculated difference between values.</returns>
    public float NormDifference(float value, int section, Patient patient);
}