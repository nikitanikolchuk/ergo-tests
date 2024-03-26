namespace TestAdministration.Models.Calculators;

/// <summary>
/// An interface for a read only collection of test norms.
/// </summary>
public interface ITestNorms
{
    /// <summary>
    /// Get corresponding norm values.
    /// </summary>
    /// <param name="section">Test's section number starting from zero.</param>
    /// <param name="patient">The tested patient.</param>
    /// <param name="age">Current patient's age.</param>
    /// <returns>A corresponding <c>TestNorm</c>.</returns>
    public TestNorm Get(int section, Patient patient, int age);
}