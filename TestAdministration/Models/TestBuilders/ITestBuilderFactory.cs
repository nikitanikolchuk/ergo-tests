using TestAdministration.Models.Data;

namespace TestAdministration.Models.TestBuilders;

/// <summary>
/// An interface for creating test specific builders.
/// </summary>
public interface ITestBuilderFactory
{
    /// <summary>
    /// Creates a test specific builder.
    /// </summary>
    /// <param name="type">The test's type.</param>
    /// <returns>A new <c>TestBuilder</c> instance.</returns>
    ITestBuilder Create(TestType type);
}