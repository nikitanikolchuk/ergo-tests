namespace TestAdministration.Models.Calculators;

/// <summary>
/// <c>ITestCalculator</c> implementation for Purdue Pegboard Test.
/// </summary>
public class PptTestCalculator : AbstractTestCalculator
{
    private static readonly TestNorm RightHandNorm = new(1.79f, 17.15f);
    private static readonly TestNorm LeftHandNorm = new(1.70f, 16.01f);
    private static readonly TestNorm BothHandsNorm = new(1.55f, 13.79f);
    private static readonly TestNorm SumNorm = new(4.04f, 47.76f);
    private static readonly TestNorm AssemblyNorm = new(5.89f, 39.30f);

    protected override TestNorm GetNorm(int section, bool isMale, int age, bool isRightDominant) => section switch
    {
        0 => isRightDominant ? RightHandNorm : LeftHandNorm,
        1 => isRightDominant ? LeftHandNorm : RightHandNorm,
        2 => BothHandsNorm,
        3 => SumNorm,
        4 => AssemblyNorm,
        _ => throw new ArgumentOutOfRangeException(
            nameof(section),
            section,
            "PPT section number not in range 0..4"
        )
    };
}