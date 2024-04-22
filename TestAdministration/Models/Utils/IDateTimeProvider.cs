namespace TestAdministration.Models.Utils;

/// <summary>
/// A wrapper class for <c>DateTime</c> for mocking current date
/// and time in tests.
/// </summary>
public interface IDateTimeProvider
{
    DateOnly Today { get; }
    TimeOnly Now { get; }
}