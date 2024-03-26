namespace TestAdministration.Models.Calculators;

/// <summary>
/// <c>ITestNorms</c> implementation for Purdue Pegboard Test.
/// </summary>
public class PptTestNorms : ITestNorms
{
    private static readonly TestNorm RightHandNorm = new(1.79f, 17.15f);
    private static readonly TestNorm LeftHandNorm = new(1.70f, 16.01f);
    private static readonly TestNorm BothHandsNorm = new(1.55f, 13.79f);
    private static readonly TestNorm TotalNorm = new(4.04f, 47.76f);
    private static readonly TestNorm AssemblyNorm = new(5.89f, 39.30f);

    public TestNorm Get(int section, Patient patient, int age) => section switch
    {
        0 => patient.DominantHand == Hand.Right ? RightHandNorm : LeftHandNorm,
        1 => patient.DominantHand == Hand.Right ? LeftHandNorm : RightHandNorm,
        2 => BothHandsNorm,
        3 => TotalNorm,
        4 => AssemblyNorm,
        _ => throw new ArgumentOutOfRangeException(
            nameof(section),
            section,
            "PPT section number not in range 0..4"
        )
    };
}