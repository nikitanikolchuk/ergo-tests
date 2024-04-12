namespace TestAdministration.Models.Utils;

public class DateProvider : IDateProvider
{
    public DateOnly Today => DateOnly.FromDateTime(DateTime.Today);
}