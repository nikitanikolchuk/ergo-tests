using TestAdministration.Models.Data;

namespace TestAdministration.Models.TestBuilders;

/// <summary>
/// Implementation of <c>ITestBuilder</c> interface.
/// Uses an <c>ITestSectionBuilder</c> for test specific
/// section creation.
/// </summary>
public class TestBuilder(
    ITestSectionBuilder sectionBuilder
) : ITestBuilder
{
    private Patient? _patient;
    private string? _tester;
    private DateOnly? _date;
    private TimeOnly? _startTime;
    private TimeOnly? _endTime;
    private readonly List<List<TestTrial>> _trials = [[]];

    public int CurrentSection => _trials.Count - 1;
    public int CurrentTrial => _trials.Last().Count;

    public bool IsFinished =>
        _trials.Count == sectionBuilder.SectionCount &&
        CurrentTrial == sectionBuilder.TrialCount;

    public ITestBuilder SetPatient(Patient patient)
    {
        _patient = patient;
        return this;
    }

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

    public ITestBuilder AddValue(float? value, string note)
    {
        if (_patient == null)
        {
            throw new InvalidOperationException("The tested patient was not set");
        }

        if (IsFinished)
        {
            throw new InvalidOperationException("All test values were already set");
        }

        if (CurrentTrial == sectionBuilder.TrialCount)
        {
            _trials.Add([]);
        }

        var trial = sectionBuilder.BuildTrial(value, note, CurrentSection, _patient);
        _trials.Last().Add(trial);

        return this;
    }

    public Test Build()
    {
        if (_patient == null)
        {
            throw new InvalidOperationException("The tested patient was not set");
        }

        if (string.IsNullOrWhiteSpace(_tester))
        {
            throw new InvalidOperationException("Tester's name was not set or is empty");
        }

        if (_date == null)
        {
            throw new InvalidOperationException("Test date was not set");
        }

        if (_startTime == null)
        {
            throw new InvalidOperationException("Test start time was not set");
        }

        if (_endTime == null)
        {
            throw new InvalidOperationException("Test end time was not set");
        }

        while (!IsFinished)
        {
            AddValue(null, string.Empty);
        }

        var sections = sectionBuilder.BuildSections(_trials, _patient);

        return new Test(
            sectionBuilder.Type,
            _tester,
            _date.Value,
            _startTime.Value,
            _endTime.Value,
            sections
        );
    }
}