namespace TestAdministration.Models;

/// <summary>
/// <c>ITesBuilder</c> implementation for Box and Block Test.
/// </summary>
public class BbtTestBuilder : AbstractTestBuilder
{
    protected override int SectionCount => 2;
    protected override int TrialCount => 4;
}