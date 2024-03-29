using TestAdministration.Models.Data;

namespace TestAdministration.Models.Calculators;

/// <summary>
/// <c>ITestNorms</c> implementation for Box and Block Test.
/// </summary>
public class BbtTestNormProvider : ITestNormProvider
{
    private static readonly SortedDictionary<int, TestNorm> MaleDominantNorms = new()
    {
        [20] = new TestNorm(8.8f, 88.2f),
        [25] = new TestNorm(7.5f, 85.0f),
        [30] = new TestNorm(9.0f, 81.9f),
        [35] = new TestNorm(9.5f, 81.9f),
        [40] = new TestNorm(8.1f, 83.0f),
        [45] = new TestNorm(9.2f, 76.9f),
        [50] = new TestNorm(9.7f, 79.0f),
        [55] = new TestNorm(11.9f, 75.2f),
        [60] = new TestNorm(8.8f, 71.3f),
        [65] = new TestNorm(7.1f, 68.4f),
        [70] = new TestNorm(9.2f, 66.3f),
        [75] = new TestNorm(7.1f, 63.0f)
    };

    private static readonly SortedDictionary<int, TestNorm> MaleNonDominantNorms = new()
    {
        [20] = new TestNorm(8.5f, 86.4f),
        [25] = new TestNorm(7.1f, 84.1f),
        [30] = new TestNorm(8.1f, 81.3f),
        [35] = new TestNorm(9.7f, 79.8f),
        [40] = new TestNorm(8.8f, 80.0f),
        [45] = new TestNorm(7.8f, 75.8f),
        [50] = new TestNorm(9.2f, 77.0f),
        [55] = new TestNorm(10.5f, 73.8f),
        [60] = new TestNorm(8.1f, 70.5f),
        [65] = new TestNorm(7.8f, 67.4f),
        [70] = new TestNorm(9.8f, 64.3f),
        [75] = new TestNorm(8.4f, 61.3f)
    };

    private static readonly SortedDictionary<int, TestNorm> FemaleDominantNorms = new()
    {
        [20] = new TestNorm(8.3f, 88.0f),
        [25] = new TestNorm(7.4f, 86.0f),
        [30] = new TestNorm(7.4f, 85.2f),
        [35] = new TestNorm(6.1f, 84.8f),
        [40] = new TestNorm(8.2f, 81.1f),
        [45] = new TestNorm(7.5f, 82.1f),
        [50] = new TestNorm(10.7f, 77.7f),
        [55] = new TestNorm(8.9f, 74.7f),
        [60] = new TestNorm(6.9f, 76.1f),
        [65] = new TestNorm(6.2f, 72.0f),
        [70] = new TestNorm(7.0f, 68.6f),
        [75] = new TestNorm(7.1f, 65.0f)
    };

    private static readonly SortedDictionary<int, TestNorm> FemaleNonDominantNorms = new()
    {
        [20] = new TestNorm(7.9f, 83.4f),
        [25] = new TestNorm(6.4f, 80.9f),
        [30] = new TestNorm(5.6f, 80.2f),
        [35] = new TestNorm(6.1f, 83.5f),
        [40] = new TestNorm(8.8f, 79.7f),
        [45] = new TestNorm(7.6f, 78.3f),
        [50] = new TestNorm(9.9f, 74.3f),
        [55] = new TestNorm(7.8f, 73.6f),
        [60] = new TestNorm(6.4f, 73.6f),
        [65] = new TestNorm(7.7f, 71.3f),
        [70] = new TestNorm(7.0f, 68.3f),
        [75] = new TestNorm(7.4f, 63.6f)
    };

    public TestNorm Get(int section, Patient patient, int age)
    {
        if (section != 0 && section != 1)
        {
            throw new ArgumentOutOfRangeException(
                nameof(section),
                section,
                "BBT section number not in range 0..1"
            );
        }

        ArgumentOutOfRangeException.ThrowIfLessThan(age, 20);

        var isDominant = section == 0;
        SortedDictionary<int, TestNorm> normDictionary;
        if (patient.IsMale)
        {
            normDictionary = isDominant ? MaleDominantNorms : MaleNonDominantNorms;
        }
        else
        {
            normDictionary = isDominant ? FemaleDominantNorms : FemaleNonDominantNorms;
        }

        return normDictionary.Last(kvp => kvp.Key <= age).Value;
    }
}