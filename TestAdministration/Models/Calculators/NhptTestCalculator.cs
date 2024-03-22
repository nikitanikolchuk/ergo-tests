namespace TestAdministration.Models.Calculators;

/// <summary>
/// <c>ITestCalculator</c> implementation for Nine Hole Peg Test.
/// </summary>
public class NhptTestCalculator : AbstractTestCalculator
{
    private static readonly SortedDictionary<int, TestNorm> MaleDominantNorms = new()
    {
        { 20, new TestNorm(1.9f, 16.1f) },
        { 25, new TestNorm(1.6f, 16.7f) },
        { 30, new TestNorm(2.5f, 17.7f) },
        { 35, new TestNorm(2.4f, 17.9f) },
        { 40, new TestNorm(2.2f, 17.7f) },
        { 45, new TestNorm(2.3f, 18.8f) },
        { 50, new TestNorm(1.8f, 19.2f) },
        { 55, new TestNorm(2.6f, 19.2f) },
        { 60, new TestNorm(2.6f, 20.3f) },
        { 65, new TestNorm(2.9f, 20.7f) },
        { 70, new TestNorm(3.3f, 22.0f) },
        { 75, new TestNorm(4.0f, 22.9f) }
    };

    private static readonly SortedDictionary<int, TestNorm> MaleNonDominantNorms = new()
    {
        { 20, new TestNorm(2.2f, 16.8f) },
        { 25, new TestNorm(1.6f, 17.7f) },
        { 30, new TestNorm(2.2f, 18.7f) },
        { 35, new TestNorm(3.5f, 19.4f) },
        { 40, new TestNorm(2.0f, 18.9f) },
        { 45, new TestNorm(3.0f, 20.4f) },
        { 50, new TestNorm(2.3f, 20.7f) },
        { 55, new TestNorm(3.2f, 21.0f) },
        { 60, new TestNorm(2.5f, 21.0f) },
        { 65, new TestNorm(3.5f, 22.9f) },
        { 70, new TestNorm(3.9f, 23.8f) },
        { 75, new TestNorm(4.8f, 26.4f) }
    };

    private static readonly SortedDictionary<int, TestNorm> FemaleDominantNorms = new()
    {
        { 20, new TestNorm(2.1f, 15.8f) },
        { 25, new TestNorm(2.2f, 15.8f) },
        { 30, new TestNorm(1.9f, 16.3f) },
        { 35, new TestNorm(1.6f, 16.4f) },
        { 40, new TestNorm(2.1f, 16.8f) },
        { 45, new TestNorm(2.0f, 17.3f) },
        { 50, new TestNorm(2.5f, 18.0f) },
        { 55, new TestNorm(2.6f, 17.8f) },
        { 60, new TestNorm(2.0f, 18.4f) },
        { 65, new TestNorm(2.3f, 19.5f) },
        { 70, new TestNorm(2.7f, 20.2f) },
        { 75, new TestNorm(2.9f, 21.5f) }
    };

    private static readonly SortedDictionary<int, TestNorm> FemaleNonDominantNorms = new()
    {
        { 20, new TestNorm(2.4f, 17.2f) },
        { 25, new TestNorm(2.1f, 17.2f) },
        { 30, new TestNorm(2.0f, 17.8f) },
        { 35, new TestNorm(2.0f, 17.3f) },
        { 40, new TestNorm(2.8f, 18.6f) },
        { 45, new TestNorm(1.9f, 18.4f) },
        { 50, new TestNorm(3.0f, 20.1f) },
        { 55, new TestNorm(2.3f, 19.4f) },
        { 60, new TestNorm(2.2f, 20.6f) },
        { 65, new TestNorm(2.7f, 21.4f) },
        { 70, new TestNorm(2.7f, 22.0f) },
        { 75, new TestNorm(4.3f, 24.6f) }
    };

    protected override TestNorm GetNorm(int section, bool isMale, int age, bool isRightDominant)
    {
        if (section != 0 && section != 1)
        {
            throw new ArgumentOutOfRangeException(
                nameof(section),
                section,
                "NHPT section number not in range 0..1"
            );
        }

        ArgumentOutOfRangeException.ThrowIfLessThan(age, 20);

        var isDominant = section == 0;
        SortedDictionary<int, TestNorm> normDictionary;
        if (isMale)
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