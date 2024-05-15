using TestAdministration.Models.Data;
using TestAdministration.Models.Utils;

namespace TestAdministration.Models.Services;

public class AgeCalculatorService(
    IDateTimeProvider dateTimeProvider
) : IAgeCalculatorService
{
    public int Calculate(Patient patient)
    {
        var today = dateTimeProvider.Today;
        var age = today.Year - patient.BirthDate.Year;
        if (today.Month < patient.BirthDate.Month ||
            (today.Month == patient.BirthDate.Month && today.Day < patient.BirthDate.Day))
        {
            age--;
        }

        return age;
    }
}