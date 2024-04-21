using TestAdministration.Models.Data;

namespace TestAdministration.Models.Storages.Mappers;

/// <summary>
/// A class for conversion between <see cref="Patient"/> and
/// <see cref="PatientCsvRecord"/> objects.
/// </summary>
public class PatientCsvConverter
{
    /// <summary>
    /// Converts a nullable CSV patient record to a non-nullable patient object.
    /// </summary>
    /// <exception cref="CsvConverterException">Thrown when one of the fields is null.</exception>
    public Patient FromRecord(PatientCsvRecord record)
    {
        if (record.Id is null)
        {
            throw new CsvConverterException("Patient record Id is null");
        }

        if (record.Name is null)
        {
            throw new CsvConverterException("Patient record Name is null");
        }

        if (record.Surname is null)
        {
            throw new CsvConverterException("Patient record Surname is null");
        }

        if (record.IsMale is null)
        {
            throw new CsvConverterException("Patient record IsMale is null");
        }

        if (record.BirthDate is null)
        {
            throw new CsvConverterException("Patient record BirthDate is null");
        }

        if (record.DominantHand is null)
        {
            throw new CsvConverterException("Patient record DominantHand is null");
        }

        if (record.PathologicalHand is null)
        {
            throw new CsvConverterException("Patient record PathologicalHand is null");
        }

        return new Patient(
            record.Id,
            record.Name,
            record.Surname,
            record.IsMale.Value,
            record.BirthDate.Value,
            record.DominantHand.Value,
            record.PathologicalHand.Value
        );
    }

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