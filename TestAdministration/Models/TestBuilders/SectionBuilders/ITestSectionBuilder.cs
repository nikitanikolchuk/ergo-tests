using System.Collections.Immutable;
using TestAdministration.Models.Data;

namespace TestAdministration.Models.TestBuilders.SectionBuilders;

/// <summary>
/// An interface for test specific section creation.
/// </summary>
public interface ITestSectionBuilder
{
    /// <summary>
    /// The test's type.
    /// </summary>
    public TestType Type { get; }

    /// <value>
    /// Number of sections to be added.
    /// </value>
    public int SectionCount { get; }

    /// <value>
    /// Number of trials to be added in each section.
    /// </value>
    public int TrialCount { get; }

    /// <summary>
    /// Creates a test trial from an added value. May be used by
    /// child classes for additional sections.
    /// </summary>
    /// <param name="value">Trial's value.</param>
    /// <param name="note">Trial's note.</param>
    /// <param name="section">
    /// Section number starting from 0.
    /// Used for calculating comparison values.
    /// </param>
    /// <param name="patient">The patient for calculating comparison values.</param>
    /// <returns>A new TestTrial.</returns>
    TestTrial BuildTrial(float? value, string note, int section, Patient patient);

    /// <summary>
    /// Creates test sections from added values.
    /// </summary>
    /// <param name="trials">2D list of added test values.</param>
    /// <param name="patient">The patient for calculating comparison values.</param>
    /// <returns>An immutable list of <c>TestSection</c> objects.</returns>
    ImmutableList<TestSection> BuildSections(List<List<TestTrial>> trials, Patient patient);
}