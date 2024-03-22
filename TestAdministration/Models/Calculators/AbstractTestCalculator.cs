namespace TestAdministration.Models.Calculators;

/// <summary>
/// Base implementation of <c>ITestCalculator</c> interface.
/// </summary>
public abstract class AbstractTestCalculator : ITestCalculator
{
    public float SdScore(float value, int section, Patient patient)
    {
        var norm = GetNorm(section, patient);
        return (value - norm.Average) / norm.Sd;
    }

    public float NormDifference(float value, int section, Patient patient)
    {
        var norm = GetNorm(section, patient);
        return value - norm.Average;
    }

    public int Age(Patient patient)
    {
        var today = DateTime.Today;
        var age = today.Year - patient.BirthDate.Year;
        if (today.Month < patient.BirthDate.Month ||
            (today.Month == patient.BirthDate.Month && today.Day < patient.BirthDate.Day))
        {
            age--;
        }

        return age;
    }

    /// <summary>
    /// Get corresponding norm values.
    /// </summary>
    /// <param name="section">Test's section number starting from zero.</param>
    /// <param name="patient">The tested patient.</param>
    /// <returns>A corresponding <c>TestNorm</c>.</returns>
    protected abstract TestNorm GetNorm(int section, Patient patient);
}