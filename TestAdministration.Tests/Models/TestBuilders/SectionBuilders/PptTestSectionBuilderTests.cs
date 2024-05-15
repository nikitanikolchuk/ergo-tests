using System.Collections.Immutable;
using Moq;
using TestAdministration.Models.Data;
using TestAdministration.Models.TestBuilders.SectionBuilders;
using TestAdministration.Models.TestBuilders.SectionBuilders.Calculators;

namespace TestAdministration.Tests.Models.TestBuilders.SectionBuilders;

public class PptTestSectionBuilderTests
{
    private readonly Mock<ITestCalculator<PptTestNormProvider>> _mockTestCalculator;
    private readonly PptTestSectionBuilder _pptTestSectionBuilder;
    private readonly Patient _patient;

    public PptTestSectionBuilderTests()
    {
        _mockTestCalculator = new Mock<ITestCalculator<PptTestNormProvider>>();
        _pptTestSectionBuilder = new PptTestSectionBuilder(_mockTestCalculator.Object);

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

        var trial = _pptTestSectionBuilder.BuildTrial(value, note, section, _patient);

        Assert.Equal(expectedTrial, trial);
    }

    [Fact]
    public void BuildTrial_ReturnsTestTrialWithNullSdScore_WhenValueIsNull()
    {
        float? value = null;
        const string note = "Note";
        const int section = 0;
        var expectedTrial = new TestTrial(value, null, note);

        var trial = _pptTestSectionBuilder.BuildTrial(value, note, section, _patient);

        Assert.Equal(expectedTrial, trial);
    }

    [Fact]
    public void BuildSections_ReturnsCorrectTestSections()
    {
        const int totalSection = 3;
        const float totalSdScore = 1.0f;
        var firstTrialList = new List<TestTrial>
        {
            new(10, 1.0f, "Note1"),
            new(15, 1.5f, "Note2"),
            new(20, 2.0f, "Note3")
        }.ToImmutableList();
        var secondTrialList = new List<TestTrial>
        {
            new(15, 1.5f, "Note4"),
            new(20, 2.0f, "Note5"),
            new(25, 2.5f, "Note6")
        }.ToImmutableList();
        var thirdTrialList = new List<TestTrial>
        {
            new(20, 2.0f, "Note7"),
            new(25, 2.5f, "Note8"),
            new(30, 3.0f, "Note9")
        }.ToImmutableList();
        var fourthTrialList = new List<TestTrial>
        {
            new(25, 2.5f, "Note10"),
            new(30, 3.0f, "Note11"),
            new(35, 3.5f, "Note12")
        }.ToImmutableList();
        var totalTrialList = new List<TestTrial>
        {
            new(45, totalSdScore, string.Empty),
            new(60, totalSdScore, string.Empty),
            new(75, totalSdScore, string.Empty)
        }.ToImmutableList();
        var trials = new List<List<TestTrial>>
        {
            firstTrialList.ToList(),
            secondTrialList.ToList(),
            thirdTrialList.ToList(),
            fourthTrialList.ToList()
        };
        var expectedSections = new List<TestSection>
        {
            new(15, 1.5f, firstTrialList),
            new(20, 2.0f, secondTrialList),
            new(25, 2.5f, thirdTrialList),
            new(60, totalSdScore, totalTrialList),
            new(30, 3.0f, fourthTrialList)
        };

        _mockTestCalculator
            .Setup(calculator => calculator.SdScore(It.IsAny<float>(), totalSection, _patient))
            .Returns(totalSdScore);

        var sections = _pptTestSectionBuilder.BuildSections(trials, _patient);

        Assert.Equal(expectedSections, sections);
    }
}