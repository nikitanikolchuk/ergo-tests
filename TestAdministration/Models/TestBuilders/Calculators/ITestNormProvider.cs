namespace TestAdministration.Models.TestBuilders.Calculators;

/// <summary>
/// An interface for getting test specific norms.
/// </summary>
public interface ITestNormProvider
{
    /// <summary>
    /// Defines if the resulting value should be multiplied by -1.
    /// </summary>
    public bool IsInverted { get; }

    /// <summary>
    /// Returns a sorted dictionary where keys are lower bounds for
    /// age categories and values are corresponding norms.
    /// </summary>
    /// <param name="section">Test section number starting from zero.</param>
    /// <param name="isMale">The patient's gender.</param>
    public SortedDictionary<int, TestNorm> GetNormDictionary(int section, bool isMale);
}