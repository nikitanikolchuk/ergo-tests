namespace TestAdministration.Models.Utils;

public class DateTimeProvider : IDateTimeProvider
{
    public DateOnly Today => DateOnly.FromDateTime(DateTime.Today);
    public TimeOnly Now => TimeOnly.FromDateTime(DateTime.Now);
}