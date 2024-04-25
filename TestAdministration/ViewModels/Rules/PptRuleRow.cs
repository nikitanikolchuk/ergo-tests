namespace TestAdministration.ViewModels.Rules;

/// <summary>
/// A class for providing bindings to a single row of values for
/// <see cref="PptRulesViewModel"/>.
/// </summary>
public class PptRuleRow(string situation, string solution, string scoring)
{
    public string Situation => situation;
    public string Solution => solution;
    public string Scoring => scoring;
}