using System.Collections.Immutable;

namespace TestAdministration.Models;

/// <summary>
/// Base implementation of <c>ITestBuilder</c> interface.
/// </summary>
public abstract class AbstractTestBuilder : ITestBuilder
{
    private string? _tester;
    private DateOnly? _date;
    private TimeOnly? _startTime;
    private TimeOnly? _endTime;
    private readonly List<List<TestTrial>> _trials = [[]];

    public int CurrentSection => _trials.Count - 1;
    public int CurrentTrial => _trials.Last().Count;
    public bool IsFinished => _trials.Count == SectionCount && CurrentTrial == TrialCount;
    
    /// <value>
    /// Number of sections to be added.
    /// </value>
    protected abstract int SectionCount { get; }

    /// <value>
    /// Number of trials to be added in each section.
    /// </value>
    protected abstract int TrialCount { get; }

    public ITestBuilder SetTester(string tester)
    {
        _tester = tester;
        return this;
    }

    public ITestBuilder SetDate(DateOnly date)
    {
        _date = date;
        return this;
    }

    public ITestBuilder SetStartTime(TimeOnly time)
    {
        _startTime = time;
        return this;
    }

    public ITestBuilder SetEndTime(TimeOnly time)
    {
        _endTime = time;
        return this;
    }

    public ITestBuilder AddValue(float? value, string? note)
    {
        if (IsFinished)
        {
            throw new InvalidOperationException("All test values were already set");
        }

        if (CurrentTrial == TrialCount)
        {
            _trials.Add([]);
        }

        var trial = new TestTrial(value, note);
        _trials.Last().Add(trial);

        return this;
    }

    public Test Build()
    {
        if (string.IsNullOrWhiteSpace(_tester))
        {
            throw new InvalidOperationException("Tester's name was not set or is empty");
        }

        if (!_date.HasValue)
        {
            throw new InvalidOperationException("Test date was not set");
        }

        if (!_startTime.HasValue)
        {
            throw new InvalidOperationException("Test start time was not set");
        }

        if (!_endTime.HasValue)
        {
            throw new InvalidOperationException("Test end time was not set");
        }

        while (!IsFinished)
        {
            AddValue(null, null);
        }

        var sections = BuildSections(_trials);

        return new Test(
            _tester,
            _date.Value,
            _startTime.Value,
            _endTime.Value,
            sections
        );
    }

    /// <summary>
    /// Creates test sections from added values.
    /// </summary>
    /// <param name="trials">2D list of added test values.</param>
    /// <returns>An immutable list of <c>TestSection</c> objects.</returns>
    protected virtual ImmutableList<TestSection> BuildSections(List<List<TestTrial>> trials) =>
        trials.Select(trialList =>
            new TestSection(trialList.ToImmutableList())
        ).ToImmutableList();
}