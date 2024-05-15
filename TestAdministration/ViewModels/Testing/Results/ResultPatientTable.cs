namespace TestAdministration.ViewModels.Testing.Results;

/// <summary>
/// A class for providing patient related bindings for
/// <see cref="ResultsViewModel"/>.
/// </summary>
public class ResultPatientTable(string fullName, int age, string dominantHand)
{
    public string LeftHeader => string.Empty;
    public string FullName => fullName;
    public string Age => age.ToString();
    public string DominantHand => dominantHand;
}