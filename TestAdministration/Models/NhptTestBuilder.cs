namespace TestAdministration.Models;

/// <summary>
/// <c>ITestBuilder</c> implementation for Nine Hole Peg Test.
/// </summary>
public class NhptTestBuilder : AbstractTestBuilder
{
    protected override int SectionCount => 2;
    protected override int TrialCount => 4;
}