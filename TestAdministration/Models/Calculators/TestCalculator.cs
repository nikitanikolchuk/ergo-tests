namespace TestAdministration.Models.Calculators;

/// <summary>
/// An interface for calculating values used in tests.
/// </summary>
public class TestCalculator(ITestNorms norms)
{
    /// <summary>
    /// Calculates Standard Deviation Score for a test value.
    /// </summary>
    /// <param name="value">The measured test value.</param>
    /// <param name="section">Test's section number starting from zero.</param>
    /// <param name="patient">The tested patient.</param>
    /// <returns>The calculated Standard Deviation Score.</returns>
    public float SdScore(float value, int section, Patient patient)
    {
        var norm = norms.Get(section, patient, Age(patient));
        return (value - norm.Average) / norm.Sd;
    }

    /// <summary>
    /// Calculates difference between a measured value and
    /// corresponding average value.
    /// </summary>
    /// <param name="value">The measured test value.</param>
    /// <param name="section">Test's section number starting from zero.</param>
    /// <param name="patient">The tested patient.</param>
    /// <returns>The calculated difference between values.</returns>
    public float NormDifference(float value, int section, Patient patient)
    {
        var norm = norms.Get(section, patient, Age(patient));
        return value - norm.Average;
    }

    /// <summary>
    /// Calculates a patient's age using current date.
    /// </summary>
    public static int Age(Patient patient)
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
}