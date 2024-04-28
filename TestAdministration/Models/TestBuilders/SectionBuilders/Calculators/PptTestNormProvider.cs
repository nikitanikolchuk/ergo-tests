namespace TestAdministration.Models.TestBuilders.SectionBuilders.Calculators;

/// <summary>
/// <see cref="ITestNormProvider"/> implementation for Purdue Pegboard Test.
/// </summary>
public class PptTestNormProvider : ITestNormProvider
{
    public bool IsInverted => false;

    public SortedDictionary<int, TestNorm> GetNormDictionary(int section, bool isMale) => section switch
    {
        0 => isMale ? MaleDominantHandNorms : FemaleDominantHandNorms,
        1 => isMale ? MaleNonDominantHandNorms : FemaleNonDominantHandNorms,
        2 => isMale ? MaleBothHandsNorms : FemaleBothHandsNorms,
        3 => isMale ? MaleTotalNorms : FemaleTotalNorms,
        4 => isMale ? MaleAssemblyNorms : FemaleAssemblyNorms,
        _ => throw new ArgumentOutOfRangeException(
            nameof(section),
            section,
            "PPT section number not in range 0..4"
        )
    };

    private static readonly SortedDictionary<int, TestNorm> MaleDominantHandNorms = new()
    {
        [20] = new TestNorm(1.79f, 17.15f),
        [25] = new TestNorm(1.79f, 17.15f),
        [30] = new TestNorm(1.79f, 17.15f),
        [35] = new TestNorm(1.79f, 17.15f),
        [40] = new TestNorm(1.79f, 17.15f),
        [45] = new TestNorm(1.79f, 17.15f),
        [50] = new TestNorm(1.79f, 17.15f),
        [55] = new TestNorm(1.79f, 17.15f),
        [60] = new TestNorm(1.79f, 17.15f),
        [65] = new TestNorm(1.79f, 17.15f),
        [70] = new TestNorm(1.79f, 17.15f),
        [75] = new TestNorm(1.79f, 17.15f)
    };

    private static readonly SortedDictionary<int, TestNorm> MaleNonDominantHandNorms = new()
    {
        [20] = new TestNorm(1.70f, 16.01f),
        [25] = new TestNorm(1.70f, 16.01f),
        [30] = new TestNorm(1.70f, 16.01f),
        [35] = new TestNorm(1.70f, 16.01f),
        [40] = new TestNorm(1.70f, 16.01f),
        [45] = new TestNorm(1.70f, 16.01f),
        [50] = new TestNorm(1.70f, 16.01f),
        [55] = new TestNorm(1.70f, 16.01f),
        [60] = new TestNorm(1.70f, 16.01f),
        [65] = new TestNorm(1.70f, 16.01f),
        [70] = new TestNorm(1.70f, 16.01f),
        [75] = new TestNorm(1.70f, 16.01f)
    };

    private static readonly SortedDictionary<int, TestNorm> MaleBothHandsNorms = new()
    {
        [20] = new TestNorm(1.55f, 13.79f),
        [25] = new TestNorm(1.55f, 13.79f),
        [30] = new TestNorm(1.55f, 13.79f),
        [35] = new TestNorm(1.55f, 13.79f),
        [40] = new TestNorm(1.55f, 13.79f),
        [45] = new TestNorm(1.55f, 13.79f),
        [50] = new TestNorm(1.55f, 13.79f),
        [55] = new TestNorm(1.55f, 13.79f),
        [60] = new TestNorm(1.55f, 13.79f),
        [65] = new TestNorm(1.55f, 13.79f),
        [70] = new TestNorm(1.55f, 13.79f),
        [75] = new TestNorm(1.55f, 13.79f)
    };

    private static readonly SortedDictionary<int, TestNorm> MaleTotalNorms = new()
    {
        [20] = new TestNorm(4.04f, 47.76f),
        [25] = new TestNorm(4.04f, 47.76f),
        [30] = new TestNorm(4.04f, 47.76f),
        [35] = new TestNorm(4.04f, 47.76f),
        [40] = new TestNorm(4.04f, 47.76f),
        [45] = new TestNorm(4.04f, 47.76f),
        [50] = new TestNorm(4.04f, 47.76f),
        [55] = new TestNorm(4.04f, 47.76f),
        [60] = new TestNorm(4.04f, 47.76f),
        [65] = new TestNorm(4.04f, 47.76f),
        [70] = new TestNorm(4.04f, 47.76f),
        [75] = new TestNorm(4.04f, 47.76f)
    };

    private static readonly SortedDictionary<int, TestNorm> MaleAssemblyNorms = new()
    {
        [20] = new TestNorm(5.89f, 39.30f),
        [25] = new TestNorm(5.89f, 39.30f),
        [30] = new TestNorm(5.89f, 39.30f),
        [35] = new TestNorm(5.89f, 39.30f),
        [40] = new TestNorm(5.89f, 39.30f),
        [45] = new TestNorm(5.89f, 39.30f),
        [50] = new TestNorm(5.89f, 39.30f),
        [55] = new TestNorm(5.89f, 39.30f),
        [60] = new TestNorm(5.89f, 39.30f),
        [65] = new TestNorm(5.89f, 39.30f),
        [70] = new TestNorm(5.89f, 39.30f),
        [75] = new TestNorm(5.89f, 39.30f)
    };

    private static readonly SortedDictionary<int, TestNorm> FemaleDominantHandNorms = new()
    {
        [20] = new TestNorm(1.79f, 17.15f),
        [25] = new TestNorm(1.79f, 17.15f),
        [30] = new TestNorm(1.79f, 17.15f),
        [35] = new TestNorm(1.79f, 17.15f),
        [40] = new TestNorm(1.79f, 17.15f),
        [45] = new TestNorm(1.79f, 17.15f),
        [50] = new TestNorm(1.79f, 17.15f),
        [55] = new TestNorm(1.79f, 17.15f),
        [60] = new TestNorm(1.79f, 17.15f),
        [65] = new TestNorm(1.79f, 17.15f),
        [70] = new TestNorm(1.79f, 17.15f),
        [75] = new TestNorm(1.79f, 17.15f)
    };

    private static readonly SortedDictionary<int, TestNorm> FemaleNonDominantHandNorms = new()
    {
        [20] = new TestNorm(1.70f, 16.01f),
        [25] = new TestNorm(1.70f, 16.01f),
        [30] = new TestNorm(1.70f, 16.01f),
        [35] = new TestNorm(1.70f, 16.01f),
        [40] = new TestNorm(1.70f, 16.01f),
        [45] = new TestNorm(1.70f, 16.01f),
        [50] = new TestNorm(1.70f, 16.01f),
        [55] = new TestNorm(1.70f, 16.01f),
        [60] = new TestNorm(1.70f, 16.01f),
        [65] = new TestNorm(1.70f, 16.01f),
        [70] = new TestNorm(1.70f, 16.01f),
        [75] = new TestNorm(1.70f, 16.01f)
    };

    private static readonly SortedDictionary<int, TestNorm> FemaleBothHandsNorms = new()
    {
        [20] = new TestNorm(1.55f, 13.79f),
        [25] = new TestNorm(1.55f, 13.79f),
        [30] = new TestNorm(1.55f, 13.79f),
        [35] = new TestNorm(1.55f, 13.79f),
        [40] = new TestNorm(1.55f, 13.79f),
        [45] = new TestNorm(1.55f, 13.79f),
        [50] = new TestNorm(1.55f, 13.79f),
        [55] = new TestNorm(1.55f, 13.79f),
        [60] = new TestNorm(1.55f, 13.79f),
        [65] = new TestNorm(1.55f, 13.79f),
        [70] = new TestNorm(1.55f, 13.79f),
        [75] = new TestNorm(1.55f, 13.79f)
    };

    private static readonly SortedDictionary<int, TestNorm> FemaleTotalNorms = new()
    {
        [20] = new TestNorm(4.04f, 47.76f),
        [25] = new TestNorm(4.04f, 47.76f),
        [30] = new TestNorm(4.04f, 47.76f),
        [35] = new TestNorm(4.04f, 47.76f),
        [40] = new TestNorm(4.04f, 47.76f),
        [45] = new TestNorm(4.04f, 47.76f),
        [50] = new TestNorm(4.04f, 47.76f),
        [55] = new TestNorm(4.04f, 47.76f),
        [60] = new TestNorm(4.04f, 47.76f),
        [65] = new TestNorm(4.04f, 47.76f),
        [70] = new TestNorm(4.04f, 47.76f),
        [75] = new TestNorm(4.04f, 47.76f)
    };

    private static readonly SortedDictionary<int, TestNorm> FemaleAssemblyNorms = new()
    {
        [20] = new TestNorm(5.89f, 39.30f),
        [25] = new TestNorm(5.89f, 39.30f),
        [30] = new TestNorm(5.89f, 39.30f),
        [35] = new TestNorm(5.89f, 39.30f),
        [40] = new TestNorm(5.89f, 39.30f),
        [45] = new TestNorm(5.89f, 39.30f),
        [50] = new TestNorm(5.89f, 39.30f),
        [55] = new TestNorm(5.89f, 39.30f),
        [60] = new TestNorm(5.89f, 39.30f),
        [65] = new TestNorm(5.89f, 39.30f),
        [70] = new TestNorm(5.89f, 39.30f),
        [75] = new TestNorm(5.89f, 39.30f)
    };
}