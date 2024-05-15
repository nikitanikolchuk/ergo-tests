using System.Collections.Immutable;
using Moq;
using TestAdministration.Models.Data;
using TestAdministration.Models.TestBuilders.SectionBuilders;
using TestAdministration.Models.TestBuilders.SectionBuilders.Calculators;

namespace TestAdministration.Tests.Models.TestBuilders.SectionBuilders;

public class NhptTestSectionBuilderTests
{
    private readonly Mock<ITestCalculator<NhptTestNormProvider>> _mockTestCalculator;
    private readonly NhptTestSectionBuilder _nhptTestSectionBuilder;
    private readonly Patient _patient;

    public NhptTestSectionBuilderTests()
    {
        _mockTestCalculator = new Mock<ITestCalculator<NhptTestNormProvider>>();
        _nhptTestSectionBuilder = new NhptTestSectionBuilder(_mockTestCalculator.Object);

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

        var trial = _nhptTestSectionBuilder.BuildTrial(value, note, section, _patient);

        Assert.Equal(expectedTrial, trial);
    }

    [Fact]
    public void BuildTrial_ReturnsTestTrialWithNullSdScore_WhenValueIsNull()
    {
        float? value = null;
        const string note = "Note";
        const int section = 0;
        var expectedTrial = new TestTrial(value, null, note);

        var trial = _nhptTestSectionBuilder.BuildTrial(value, note, section, _patient);

        Assert.Equal(expectedTrial, trial);
    }

    [Fact]
    public void BuildSections_ReturnsCorrectTestSections()
    {
        var firstTrialList = new List<TestTrial>
        {
            new(5, 2.5f, "Note1"),
            new(10, 2.0f, "Note2"),
            new(15, 1.5f, "Note3"),
            new(20, 1.0f, "Note4")
        }.ToImmutableList();
        var secondTrialList = new List<TestTrial>
        {
            new(10, 2.0f, "Note5"),
            new(15, 1.5f, "Note6"),
            new(20, 1.0f, "Note7"),
            new(25, 0.5f, "Note8")
        }.ToImmutableList();
        var trials = new List<List<TestTrial>>
        {
            firstTrialList.ToList(),
            secondTrialList.ToList()
        };
        var expectedSections = new List<TestSection>
        {
            new(15, 1.5f, firstTrialList),
            new(20, 1.0f, secondTrialList)
        };

        var sections = _nhptTestSectionBuilder.BuildSections(trials, _patient);

        Assert.Equal(expectedSections, sections);
    }
}