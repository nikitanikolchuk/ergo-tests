using System.ComponentModel;
using CsvHelper.Configuration;
using TestAdministration.Models.Data;
using static TestAdministration.Models.Mappers.CsvMapperConfiguration;

namespace TestAdministration.Models.Mappers;

/// <summary>
/// <c>ClassMap</c> implementation for <c>Patient</c> objects.
/// </summary>
public sealed class PatientCsvMapper : ClassMap<Patient>
{
    public PatientCsvMapper()
    {
        Map(p => p.Id).Name("Rodne_cislo");
        Map(p => p.IsMale).Name("Pohlavi").Convert(args =>
            args.Value.IsMale ? "m" : "ž"
        );
        Map(p => p.BirthDate).Name("Datum_narozeni").Convert(args =>
            args.Value.BirthDate.ToString(DateFormat)
        );
        Map(p => p.DominantHand).Name("Dominantni_HK").Convert(args =>
            _handString(args.Value.DominantHand)
        );
        Map(p => p.PathologicalHand).Name("HK_s_patologii").Convert(args =>
            _handString(args.Value.PathologicalHand)
        );
    }

    private static string _handString(Hand hand) => hand switch
    {
        Hand.Left => "levá",
        Hand.Right => "pravá",
        Hand.Both => "obě",
        _ => throw new InvalidEnumArgumentException(
            nameof(hand),
            Convert.ToInt32(hand),
            typeof(Hand)
        )
    };
}