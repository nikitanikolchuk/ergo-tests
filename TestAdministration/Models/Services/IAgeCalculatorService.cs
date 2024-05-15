using TestAdministration.Models.Data;

namespace TestAdministration.Models.Services;

public interface IAgeCalculatorService
{
    public int Calculate(Patient patient);
}