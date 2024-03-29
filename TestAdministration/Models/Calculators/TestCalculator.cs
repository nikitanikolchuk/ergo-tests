using TestAdministration.Models.Data;

namespace TestAdministration.Models.Calculators;

/// <summary>
/// An implementation of <c>ITestCalculator</c>.
/// </summary>
public class TestCalculator<T>(
    T normProvider
) : ITestCalculator<T> where T : ITestNormProvider
{
    public float SdScore(float value, int section, Patient patient)
    {
        var norm = normProvider.Get(section, patient, _age(patient));
        return (value - norm.Average) / norm.Sd;
    }

    public float NormDifference(float value, int section, Patient patient)
    {
        var norm = normProvider.Get(section, patient, _age(patient));
        return value - norm.Average;
    }

    private static int _age(Patient patient)
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