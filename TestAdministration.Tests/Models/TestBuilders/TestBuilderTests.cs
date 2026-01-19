using System.Collections.Immutable;
using Moq;
using TestAdministration.Models.Data;
using TestAdministration.Models.TestBuilders;
using TestAdministration.Models.TestBuilders.SectionBuilders;

namespace TestAdministration.Tests.Models.TestBuilders;

public class TestBuilderTests
{
    private const string Tester = "Tester";
    private const int DefaultTrialCount = 3;

    private readonly Mock<ITestSectionBuilder> _mockSectionBuilder;
    private readonly Patient _patient;
    private readonly DateOnly _date;
    private readonly TimeOnly _startTime;
    private readonly TimeOnly _endTime;

    public TestBuilderTests()
    {
        _mockSectionBuilder = new Mock<ITestSectionBuilder>();
        _mockSectionBuilder.SetupGet(sectionBuilder => sectionBuilder.Type).Returns(TestType.Nhpt);
        _mockSectionBuilder.SetupGet(sectionBuilder => sectionBuilder.SectionCount).Returns(2);
        _mockSectionBuilder.SetupGet(sectionBuilder => sectionBuilder.HasPracticeTrial).Returns(true);

        _patient = new Patient(
            "Id",
            "Name",
            "Surname",
            true,
            DateOnly.MinValue,
            Hand.Right,
            Hand.Right
        );
        _date = new DateOnly(2000, 1, 1);
        _startTime = new TimeOnly(12, 0);
        _endTime = new TimeOnly(12, 30);
    }

    [Fact]
    public void Build_BuildsCorrectly()
    {
        var trials = new List<List<TestTrial>>
        {
            new()
            {
                new TestTrial(10f, null, "Note1"),
                new TestTrial(15f, null, "Note2"),
                new TestTrial(20f, null, "Note3"),
                new TestTrial(25f, null, "Note4")
            },
            new()
            {
                new TestTrial(15f, null, "Note5"),
                new TestTrial(20f, null, "Note6"),
                new TestTrial(25f, null, "Note7"),
                new TestTrial(30f, null, "Note8")
            }
        };
        var sections = new List<TestSection>
        {
            new(20f, null, trials[0].ToImmutableList()),
            new(25f, null, trials[1].ToImmutableList())
        }.ToImmutableList();
        var expectedTest = new Test(
            TestType.Nhpt,
            Tester,
            _date,
            _startTime,
            _endTime,
            sections
        );

        _mockSectionBuilder
            .Setup(sectionBuilder =>
                sectionBuilder.BuildTrial(
                    It.IsAny<float?>(),
                    It.IsAny<string>(),
                    It.IsAny<int>(),
                    _patient
                )
            )
            .Returns((float? value, string note, int _, Patient _) =>
                new TestTrial(value, null, note)
            );
        _mockSectionBuilder
            .Setup(sectionBuilder =>
                sectionBuilder.BuildSections(It.IsAny<List<List<TestTrial>>>(), _patient)
            )
            .Returns(sections);

        var test = new TestBuilder(_mockSectionBuilder.Object)
            .SetTester(Tester)
            .SetPatient(_patient)
            .SetTrialCount(DefaultTrialCount)
            .SetDate(_date)
            .SetStartTime(_startTime)
            .SetEndTime(_endTime)
            .AddValue(trials[0][0].Value, trials[0][0].Note)
            .AddValue(trials[0][1].Value, trials[0][1].Note)
            .AddValue(trials[0][2].Value, trials[0][2].Note)
            .AddValue(trials[0][3].Value, trials[0][3].Note)
            .AddValue(trials[1][0].Value, trials[1][0].Note)
            .AddValue(trials[1][1].Value, trials[1][1].Note)
            .AddValue(trials[1][2].Value, trials[1][2].Note)
            .AddValue(trials[1][3].Value, trials[1][3].Note)
            .Build();

        _mockSectionBuilder.Verify(
            sectionBuilder => sectionBuilder.BuildTrial(
                trials[0][0].Value,
                trials[0][0].Note,
                0,
                _patient
            ),
            Times.Once
        );
        _mockSectionBuilder.Verify(
            sectionBuilder => sectionBuilder.BuildTrial(
                trials[0][1].Value,
                trials[0][1].Note,
                0,
                _patient
            ),
            Times.Once
        );
        _mockSectionBuilder.Verify(
            sectionBuilder => sectionBuilder.BuildTrial(
                trials[0][2].Value,
                trials[0][2].Note,
                0,
                _patient
            ),
            Times.Once
        );
        _mockSectionBuilder.Verify(
            sectionBuilder => sectionBuilder.BuildTrial(
                trials[0][3].Value,
                trials[0][3].Note,
                0,
                _patient
            ),
            Times.Once
        );
        _mockSectionBuilder.Verify(
            sectionBuilder => sectionBuilder.BuildTrial(
                trials[1][0].Value,
                trials[1][0].Note,
                1,
                _patient
            ),
            Times.Once
        );
        _mockSectionBuilder.Verify(
            sectionBuilder => sectionBuilder.BuildTrial(
                trials[1][1].Value,
                trials[1][1].Note,
                1,
                _patient
            ),
            Times.Once
        );
        _mockSectionBuilder.Verify(
            sectionBuilder => sectionBuilder.BuildTrial(
                trials[1][2].Value,
                trials[1][2].Note,
                1,
                _patient
            ),
            Times.Once
        );
        _mockSectionBuilder.Verify(
            sectionBuilder => sectionBuilder.BuildTrial(
                trials[1][3].Value,
                trials[1][3].Note,
                1,
                _patient
            ),
            Times.Once
        );
        _mockSectionBuilder.Verify(
            sectionBuilder => sectionBuilder.BuildTrial(
                It.IsAny<float?>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                _patient
            ),
            Times.Exactly(8)
        );
        _mockSectionBuilder.Verify(
            sectionBuilder => sectionBuilder.BuildSections(It.IsAny<List<List<TestTrial>>>(), _patient),
            Times.Once
        );
        Assert.Equal(expectedTest, test);
    }

    [Fact]
    public void Build_BuildsCorrectly_WhenTrialCountIsOne()
    {
        const int trialCount = 1;
        var trials = new List<List<TestTrial>>
        {
            new()
            {
                new TestTrial(10f, null, "Note1"),
                new TestTrial(15f, null, "Note2")
            },
            new()
            {
                new TestTrial(15f, null, "Note3"),
                new TestTrial(20f, null, "Note4")
            }
        };
        var sections = new List<TestSection>
        {
            new(15f, null, trials[0].ToImmutableList()),
            new(20f, null, trials[1].ToImmutableList())
        }.ToImmutableList();
        var expectedTest = new Test(
            TestType.Nhpt,
            Tester,
            _date,
            _startTime,
            _endTime,
            sections
        );
        _mockSectionBuilder
            .Setup(sectionBuilder =>
                sectionBuilder.BuildTrial(
                    It.IsAny<float?>(),
                    It.IsAny<string>(),
                    It.IsAny<int>(),
                    _patient
                )
            )
            .Returns((float? value, string note, int _, Patient _) =>
                new TestTrial(value, null, note)
            );
        _mockSectionBuilder
            .Setup(sectionBuilder =>
                sectionBuilder.BuildSections(It.IsAny<List<List<TestTrial>>>(), _patient)
            )
            .Returns(sections);

        var test = new TestBuilder(_mockSectionBuilder.Object)
            .SetTester(Tester)
            .SetPatient(_patient)
            .SetTrialCount(trialCount)
            .SetDate(_date)
            .SetStartTime(_startTime)
            .SetEndTime(_endTime)
            .AddValue(trials[0][0].Value, trials[0][0].Note)
            .AddValue(trials[0][1].Value, trials[0][1].Note)
            .AddValue(trials[1][0].Value, trials[1][0].Note)
            .AddValue(trials[1][1].Value, trials[1][1].Note)
            .Build();

        _mockSectionBuilder.Verify(
            sectionBuilder => sectionBuilder.BuildTrial(
                trials[0][0].Value,
                trials[0][0].Note,
                0,
                _patient
            ),
            Times.Once
        );
        _mockSectionBuilder.Verify(
            sectionBuilder => sectionBuilder.BuildTrial(
                trials[0][1].Value,
                trials[0][1].Note,
                0,
                _patient
            ),
            Times.Once
        );
        _mockSectionBuilder.Verify(
            sectionBuilder => sectionBuilder.BuildTrial(
                trials[1][0].Value,
                trials[1][0].Note,
                1,
                _patient
            ),
            Times.Once
        );
        _mockSectionBuilder.Verify(
            sectionBuilder => sectionBuilder.BuildTrial(
                trials[1][1].Value,
                trials[1][1].Note,
                1,
                _patient
            ),
            Times.Once
        );
        _mockSectionBuilder.Verify(
            sectionBuilder => sectionBuilder.BuildTrial(
                It.IsAny<float?>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                _patient
            ),
            Times.Exactly(4)
        );
        _mockSectionBuilder.Verify(
            sectionBuilder => sectionBuilder.BuildSections(It.IsAny<List<List<TestTrial>>>(), _patient),
            Times.Once
        );
        Assert.Equal(expectedTest, test);
    }

    [Fact]
    public void Build_BuildsCorrectly_WhenTrialCountIsTwo()
    {
        const int trialCount = 2;
        var trials = new List<List<TestTrial>>
        {
            new()
            {
                new TestTrial(10f, null, "Note1"),
                new TestTrial(15f, null, "Note2"),
                new TestTrial(20f, null, "Note3")
            },
            new()
            {
                new TestTrial(15f, null, "Note4"),
                new TestTrial(20f, null, "Note5"),
                new TestTrial(25f, null, "Note6")
            }
        };
        var sections = new List<TestSection>
        {
            new(17.5f, null, trials[0].ToImmutableList()),
            new(22.5f, null, trials[1].ToImmutableList())
        }.ToImmutableList();
        var expectedTest = new Test(
            TestType.Nhpt,
            Tester,
            _date,
            _startTime,
            _endTime,
            sections
        );
        _mockSectionBuilder
            .Setup(sectionBuilder =>
                sectionBuilder.BuildTrial(
                    It.IsAny<float?>(),
                    It.IsAny<string>(),
                    It.IsAny<int>(),
                    _patient
                )
            )
            .Returns((float? value, string note, int _, Patient _) =>
                new TestTrial(value, null, note)
            );
        _mockSectionBuilder
            .Setup(sectionBuilder =>
                sectionBuilder.BuildSections(It.IsAny<List<List<TestTrial>>>(), _patient)
            )
            .Returns(sections);

        var test = new TestBuilder(_mockSectionBuilder.Object)
            .SetTester(Tester)
            .SetPatient(_patient)
            .SetTrialCount(trialCount)
            .SetDate(_date)
            .SetStartTime(_startTime)
            .SetEndTime(_endTime)
            .AddValue(trials[0][0].Value, trials[0][0].Note)
            .AddValue(trials[0][1].Value, trials[0][1].Note)
            .AddValue(trials[0][2].Value, trials[0][2].Note)
            .AddValue(trials[1][0].Value, trials[1][0].Note)
            .AddValue(trials[1][1].Value, trials[1][1].Note)
            .AddValue(trials[1][2].Value, trials[1][2].Note)
            .Build();

        _mockSectionBuilder.Verify(
            sectionBuilder => sectionBuilder.BuildTrial(
                trials[0][0].Value,
                trials[0][0].Note,
                0,
                _patient
            ),
            Times.Once
        );
        _mockSectionBuilder.Verify(
            sectionBuilder => sectionBuilder.BuildTrial(
                trials[0][1].Value,
                trials[0][1].Note,
                0,
                _patient
            ),
            Times.Once
        );
        _mockSectionBuilder.Verify(
            sectionBuilder => sectionBuilder.BuildTrial(
                trials[0][2].Value,
                trials[0][2].Note,
                0,
                _patient
            ),
            Times.Once
        );
        _mockSectionBuilder.Verify(
            sectionBuilder => sectionBuilder.BuildTrial(
                trials[1][0].Value,
                trials[1][0].Note,
                1,
                _patient
            ),
            Times.Once
        );
        _mockSectionBuilder.Verify(
            sectionBuilder => sectionBuilder.BuildTrial(
                trials[1][1].Value,
                trials[1][1].Note,
                1,
                _patient
            ),
            Times.Once
        );
        _mockSectionBuilder.Verify(
            sectionBuilder => sectionBuilder.BuildTrial(
                trials[1][2].Value,
                trials[1][2].Note,
                1,
                _patient
            ),
            Times.Once
        );
        _mockSectionBuilder.Verify(
            sectionBuilder => sectionBuilder.BuildTrial(
                It.IsAny<float?>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                _patient
            ),
            Times.Exactly(6)
        );
        _mockSectionBuilder.Verify(
            sectionBuilder => sectionBuilder.BuildSections(It.IsAny<List<List<TestTrial>>>(), _patient),
            Times.Once
        );
        Assert.Equal(expectedTest, test);
    }

    [Fact]
    public void Build_ThrowsInvalidOperationException_WhenPatientNotSet()
    {
        var testBuilder = new TestBuilder(_mockSectionBuilder.Object)
            .SetTester(Tester)
            .SetTrialCount(DefaultTrialCount)
            .SetDate(_date)
            .SetStartTime(_startTime)
            .SetEndTime(_endTime);

        Assert.Throws<InvalidOperationException>(() => testBuilder.Build());
    }

    [Fact]
    public void Build_ThrowsInvalidOperationException_WhenTrialCountNotSet()
    {
        var testBuilder = new TestBuilder(_mockSectionBuilder.Object)
            .SetTester(Tester)
            .SetPatient(_patient)
            .SetDate(_date)
            .SetStartTime(_startTime)
            .SetEndTime(_endTime);

        Assert.Throws<InvalidOperationException>(() => testBuilder.Build());
    }

    [Fact]
    public void Build_ThrowsInvalidOperationException_WhenTesterNotSet()
    {
        var testBuilder = new TestBuilder(_mockSectionBuilder.Object)
            .SetPatient(_patient)
            .SetTrialCount(DefaultTrialCount)
            .SetDate(_date)
            .SetStartTime(_startTime)
            .SetEndTime(_endTime);

        Assert.Throws<InvalidOperationException>(() => testBuilder.Build());
    }

    [Fact]
    public void Build_ThrowsInvalidOperationException_WhenDateNotSet()
    {
        var testBuilder = new TestBuilder(_mockSectionBuilder.Object)
            .SetTester(Tester)
            .SetPatient(_patient)
            .SetTrialCount(DefaultTrialCount)
            .SetStartTime(_startTime)
            .SetEndTime(_endTime);

        Assert.Throws<InvalidOperationException>(() => testBuilder.Build());
    }

    [Fact]
    public void Build_ThrowsInvalidOperationException_WhenStartTimeNotSet()
    {
        var testBuilder = new TestBuilder(_mockSectionBuilder.Object)
            .SetTester(Tester)
            .SetPatient(_patient)
            .SetTrialCount(DefaultTrialCount)
            .SetDate(_date)
            .SetEndTime(_endTime);

        Assert.Throws<InvalidOperationException>(() => testBuilder.Build());
    }

    [Fact]
    public void Build_ThrowsInvalidOperationException_WhenEndTimeNotSet()
    {
        var testBuilder = new TestBuilder(_mockSectionBuilder.Object)
            .SetTester(Tester)
            .SetPatient(_patient)
            .SetTrialCount(DefaultTrialCount)
            .SetDate(_date)
            .SetStartTime(_startTime);

        Assert.Throws<InvalidOperationException>(() => testBuilder.Build());
    }

    [Fact]
    public void Build_AddsNullValuesToFinishTest()
    {
        var testBuilder = new TestBuilder(_mockSectionBuilder.Object)
            .SetTester(Tester)
            .SetPatient(_patient)
            .SetTrialCount(DefaultTrialCount)
            .SetDate(_date)
            .SetStartTime(_startTime)
            .SetEndTime(_endTime);

        _mockSectionBuilder
            .Setup(sectionBuilder =>
                sectionBuilder.BuildSections(It.IsAny<List<List<TestTrial>>>(), _patient)
            )
            .Returns(ImmutableList<TestSection>.Empty);

        _ = testBuilder.Build();

        _mockSectionBuilder.Verify(
            sectionBuilder =>
                sectionBuilder.BuildTrial(null, string.Empty, It.IsAny<int>(), _patient),
            Times.Exactly(8)
        );
    }
}