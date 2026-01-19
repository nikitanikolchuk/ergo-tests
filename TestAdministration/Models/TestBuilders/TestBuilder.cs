using TestAdministration.Models.Data;
using TestAdministration.Models.TestBuilders.SectionBuilders;

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
    private const int MaxTrialCount = 3;

    private readonly int _maxTotalTrialCount = MaxTrialCount + (sectionBuilder.HasPracticeTrial ? 1 : 0);
    private readonly List<List<TestTrial>> _trials = [[]];

    private Patient? _patient;
    private int? _totalTrialCount;
    private string? _tester;
    private DateOnly? _date;
    private TimeOnly? _startTime;
    private TimeOnly? _endTime;

    public TestType Type => sectionBuilder.Type;
    public int CurrentSection => _trials.Count - 1;
    public int CurrentTrial => _trials.Last().Count;

    public int TotalTrialCount =>
        _totalTrialCount ?? throw new InvalidOperationException("The trial count was not set");

    public bool IsFinished =>
        _trials.Count == sectionBuilder.SectionCount &&
        CurrentTrial == TotalTrialCount;

    public ITestBuilder SetPatient(Patient patient)
    {
        _patient = patient;
        return this;
    }

    public ITestBuilder SetTrialCount(int trialCount)
    {
        _totalTrialCount = trialCount + (sectionBuilder.HasPracticeTrial ? 1 : 0);
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

        TestTrial trial;
        // TODO: remove after creating practice trial norms
        // SD score isn't calculated for first practice trial
        if (sectionBuilder is NhptTestSectionBuilder or BbtTestSectionBuilder && CurrentTrial == 0)
        {
            trial = new TestTrial(value, null, note);
        }
        else
        {
            trial = sectionBuilder.BuildTrial(value, note, CurrentSection, _patient);
        }

        _trials.Last().Add(trial);

        if (!IsFinished && CurrentTrial == TotalTrialCount)
        {
            _trials.Add([]);
        }

        return this;
    }

    public ITestBuilder RemoveValue(out float? value, out string note)
    {
        if (_trials.Count <= 1 && _trials.Last().Count == 0)
        {
            value = null;
            note = string.Empty;
            return this;
        }

        if (_trials.Last().Count == 0)
        {
            _trials.RemoveAt(_trials.Count - 1);
        }

        value = _trials.Last().Last().Value;
        note = _trials.Last().Last().Note;
        _trials.Last().RemoveAt(_trials.Last().Count - 1);
        return this;
    }

    public Test Build()
    {
        if (_patient == null)
        {
            throw new InvalidOperationException("The tested patient was not set");
        }

        if (_totalTrialCount == null)
        {
            throw new InvalidOperationException("The trial count was not set");
        }

        if (_tester == null)
        {
            throw new InvalidOperationException("Tester's name was not set");
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

        foreach (var sectionTrials in _trials)
        {
            while (sectionTrials.Count < _maxTotalTrialCount)
            {
                var emptyTrial = new TestTrial(null, null, string.Empty);
                sectionTrials.Add(emptyTrial);
            }
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