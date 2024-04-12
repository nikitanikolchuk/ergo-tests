namespace TestAdministration.Models.Utils;

/// <summary>
/// A wrapper class for <c>DateTime</c> for mocking current date
/// in tests.
/// </summary>
public interface IDateProvider
{
    DateOnly Today { get; }
}