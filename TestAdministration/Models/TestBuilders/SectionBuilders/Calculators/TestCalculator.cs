using TestAdministration.Models.Data;
using TestAdministration.Models.Services;

namespace TestAdministration.Models.TestBuilders.SectionBuilders.Calculators;

public class TestCalculator<T>(
    AgeCalculatorService ageCalculatorService,
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

    private TestNorm? _getNorm(int section, Patient patient)
    {
        var age = ageCalculatorService.Calculate(patient);
        if (age < MinAge)
        {
            return null;
        }

        return normProvider.GetNormDictionary(section, patient.IsMale)
            .Last(keyValuePair => keyValuePair.Key <= age)
            .Value;
    }
}