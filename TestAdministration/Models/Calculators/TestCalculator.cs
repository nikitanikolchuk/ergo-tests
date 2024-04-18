using TestAdministration.Models.Data;
using TestAdministration.Models.Utils;

namespace TestAdministration.Models.Calculators;

public class TestCalculator<T>(
    IDateProvider dateProvider,
    T normProvider
) : ITestCalculator<T> where T : ITestNormProvider
{
    private const int MinAge = 20;

    public float? SdScore(float value, int section, Patient patient)
    {
        var norm = _getNorm(section, patient);
        var sdScore = (value - norm?.Average) / norm?.Sd;
        return normProvider.IsInverted ? -sdScore : sdScore;
    }

    public float? NormDifference(float value, int section, Patient patient)
    {
        var norm = _getNorm(section, patient);
        var normDifference = value - norm?.Average;
        return normProvider.IsInverted ? -normDifference : normDifference;
    }

    private TestNorm? _getNorm(int section, Patient patient)
    {
        var age = _age(patient);
        if (age < MinAge)
        {
            return null;
        }

        return normProvider.GetNormDictionary(section, patient.IsMale)
            .Last(keyValuePair => keyValuePair.Key <= age)
            .Value;
    }

    private int _age(Patient patient)
    {
        var today = dateProvider.Today;
        var age = today.Year - patient.BirthDate.Year;
        if (today.Month < patient.BirthDate.Month ||
            (today.Month == patient.BirthDate.Month && today.Day < patient.BirthDate.Day))
        {
            age--;
        }

        return age;
    }
}