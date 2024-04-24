using TestAdministration.Models.Data;
using TestAdministration.Models.Storages.Records;

namespace TestAdministration.Models.Storages.Converters;

/// <summary>
/// A class for conversion between <see cref="Patient"/> and
/// <see cref="PatientCsvRecord"/> objects.
/// </summary>
public class PatientCsvConverter
{
    public Patient FromRecord(PatientCsvRecord record) => new(
        record.Id,
        record.Name,
        record.Surname,
        record.IsMale,
        record.BirthDate,
        record.DominantHand,
        record.PathologicalHand
    );

    public PatientCsvRecord ToRecord(Patient patient) => new()
    {
        Id = patient.Id,
        Name = patient.Name,
        Surname = patient.Surname,
        IsMale = patient.IsMale,
        BirthDate = patient.BirthDate,
        DominantHand = patient.DominantHand,
        PathologicalHand = patient.PathologicalHand,
    };
}