using System.Collections.Immutable;
using Moq;
using TestAdministration.Models.Data;
using TestAdministration.Models.TestBuilders.SectionBuilders;
using TestAdministration.Models.TestBuilders.SectionBuilders.Calculators;

namespace TestAdministration.Tests.Models.TestBuilders.SectionBuilders;

public class BbtTestSectionBuilderTests
{
    private readonly Mock<ITestCalculator<BbtTestNormProvider>> _mockTestCalculator;
    private readonly BbtTestSectionBuilder _bbtTestSectionBuilder;
    private readonly Patient _patient;

    public BbtTestSectionBuilderTests()
    {
        _mockTestCalculator = new Mock<ITestCalculator<BbtTestNormProvider>>();
        _bbtTestSectionBuilder = new BbtTestSectionBuilder(_mockTestCalculator.Object);

        _patient = new Patient(
            "Id",
            "Name",
            "Surname",
            true,
            DateOnly.MinValue,
            Hand.Right,
            Hand.Right
        );
    }

    [Fact]
    public void BuildTrial_ReturnsCorrectTestTrial_WhenValueIsNotNull()
    {
        const float value = 10f;
        const string note = "Note";
        const float sdScore = 1f;
        const int section = 0;
        var expectedTrial = new TestTrial(value, sdScore, note);

        _mockTestCalculator
            .Setup(calculator => calculator.SdScore(value, section, _patient))
            .Returns(sdScore);

        var trial = _bbtTestSectionBuilder.BuildTrial(value, note, section, _patient);

        Assert.Equal(expectedTrial, trial);
    }

    [Fact]
    public void BuildTrial_ReturnsTestTrialWithNullSdScore_WhenValueIsNull()
    {
        float? value = null;
        const string note = "Note";
        const int section = 0;
        var expectedTrial = new TestTrial(value, null, note);

        var trial = _bbtTestSectionBuilder.BuildTrial(value, note, section, _patient);

        Assert.Equal(expectedTrial, trial);
    }

    [Fact]
    public void BuildSections_ReturnsCorrectTestSections()
    {
        var firstTrialList = new List<TestTrial>
        {
            new(10, 1.0f, "Note1"),
            new(15, 1.5f, "Note2"),
            new(20, 2.0f, "Note3"),
            new(25, 2.5f, "Note4")
        }.ToImmutableList();
        var secondTrialList = new List<TestTrial>
        {
            new(15, 1.5f, "Note5"),
            new(20, 2.0f, "Note6"),
            new(25, 2.5f, "Note7"),
            new(30, 3.0f, "Note8")
        }.ToImmutableList();
        var trials = new List<List<TestTrial>>
        {
            firstTrialList.ToList(),
            secondTrialList.ToList()
        };
        var expectedSections = new List<TestSection>
        {
            new(20, 2.0f, firstTrialList),
            new(25, 2.5f, secondTrialList)
        };

        var sections = _bbtTestSectionBuilder.BuildSections(trials, _patient);

        Assert.Equal(expectedSections, sections);
    }
}