using TestAdministration.Models.Data;

namespace TestAdministration.Models.TestBuilders;

/// <summary>
/// An interface for step-by-step addition of test values.
/// </summary>
public interface ITestBuilder
{
    /// <summary>
    /// Type of the constructed test.
    /// </summary>
    TestType Type { get; }

    /// <summary>
    /// Number of current test section starting from 0.
    /// </summary>
    int CurrentSection { get; }

    /// <summary>
    /// Number of current test trial starting from 0.
    /// </summary>
    int CurrentTrial { get; }

    /// <summary>
    /// Total trial count (including possible practice trial).
    /// Should only be accessed after using <see cref="SetTrialCount"/>.
    /// </summary>
    int TotalTrialCount { get; }

    /// <value>
    /// Is true if all test values were added.
    /// </value>
    bool IsFinished { get; }

    /// <summary>
    /// Sets the tested patient for calculating comparison values.
    /// </summary>
    /// <param name="patient">The tested patient.</param>
    /// <returns>The updated builder instance.</returns>
    ITestBuilder SetPatient(Patient patient);

    /// <summary>
    /// Sets the trial count for the test (excluding practice trial).
    /// </summary>
    /// <param name="trialCount">The trial count.</param>
    /// <returns>The updated builder instance.</returns>
    ITestBuilder SetTrialCount(int trialCount);

    /// <summary>
    /// Sets tester's name.
    /// </summary>
    /// <param name="tester">The tester's name.</param>
    /// <returns>The updated builder instance.</returns>
    ITestBuilder SetTester(string tester);

    /// <summary>
    /// Sets testing date.
    /// </summary>
    /// <param name="date">The testing date.</param>
    /// <returns>The updated builder instance.</returns>
    ITestBuilder SetDate(DateOnly date);

    /// <summary>
    /// Sets test's start time.
    /// </summary>
    /// <param name="time">The test's start time.</param>
    /// <returns>The updated builder instance.</returns>
    ITestBuilder SetStartTime(TimeOnly time);

    /// <summary>
    /// Sets test's end time.
    /// </summary>
    /// <param name="time">The test's end time.</param>
    /// <returns>The updated builder instance.</returns>
    ITestBuilder SetEndTime(TimeOnly time);

    /// <summary>
    /// Adds a test trial's value to the position specified by
    /// <c>CurrentSection</c> and <c>CurrentTrial</c> properties.
    /// </summary>
    /// <param name="value">The current trial's value.</param>
    /// <param name="note">The current trial's note.</param>
    /// <returns>The updated builder instance.</returns>
    /// <exception cref="InvalidOperationException">Thrown if <c>IsFinished</c> is true.</exception>
    ITestBuilder AddValue(float? value, string note);

    /// <summary>
    /// Removes the last added value and note if there is one.
    /// Returns removed values as out parameters.
    /// </summary>
    ITestBuilder RemoveValue(out float? value, out string note);

    /// <summary>
    /// Creates a <c>Test</c> instance using previously set values.
    /// Fills missing test values and notes with null.
    /// </summary>
    /// <returns>A new <c>Test</c> instance.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when any of required <c>Test</c> fields is not set.
    /// That does not include those set by <c>AddValue</c> method.
    /// </exception>
    Test Build();
}