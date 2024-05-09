namespace TestAdministration.Models.Storages.Converters;

public class NormInterpretationConverter
{
    public string Convert(float? sdScore) => sdScore switch
    {
        null => string.Empty,
        <= -2.0f => "statisticky významně podprůměrný výsledek",
        > -2.0f and <= -1.5f => "vysoký podprůměr",
        > -1.5f and <= -0.75f => "podprůměr",
        > -0.75f and < 0.75f => "norma",
        >= 0.75f and < 1.5f => "nadprůměr",
        >= 1.5f and < 2.0f => "vysoký nadprůměr",
        _ => "statisticky významně nadprůměrný výsledek"
    };
}