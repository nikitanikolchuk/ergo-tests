using Moq;
using TestAdministration.Models.Data;
using TestAdministration.Models.Services;
using TestAdministration.Models.TestBuilders.SectionBuilders.Calculators;

namespace TestAdministration.Tests.Models.TestBuilders.SectionBuilders.Calculators;

public class TestCalculatorTests
{
    private readonly Mock<IAgeCalculatorService> _mockAgeCalculatorService;
    private readonly Mock<ITestNormProvider> _mockNormProvider;
    private readonly TestCalculator<ITestNormProvider> _testCalculator;

    public TestCalculatorTests()
    {
        _mockAgeCalculatorService = new Mock<IAgeCalculatorService>();
        _mockNormProvider = new Mock<ITestNormProvider>();
        _testCalculator = new TestCalculator<ITestNormProvider>(
            _mockAgeCalculatorService.Object,
            _mockNormProvider.Object
        );
    }

    [Theory]
    [InlineData(19, 10, false, null)]
    [InlineData(20, 10, false, 1.0f)]
    [InlineData(23, 10, false, 1.0f)]
    [InlineData(25, 10, false, 2.0f)]
    [InlineData(25, 10, true, -2.0f)]
    public void SdScore_ReturnsExpectedSdScore(int age, float value, bool isInverted, float? expectedSdScore)
    {
        var patient = new Patient(
            "Id",
            "Name",
            "Surname",
            true,
            DateOnly.MinValue,
            Hand.Right,
            Hand.Right
        );
        var normDictionary = new SortedDictionary<int, TestNorm>
        {
            [20] = new(1, 9),
            [25] = new(0.5f, 9)
        };

        _mockAgeCalculatorService
            .Setup(service => service.Calculate(patient))
            .Returns(age);
        _mockNormProvider
            .Setup(provider => provider.GetNormDictionary(It.IsAny<int>(), It.IsAny<bool>()))
            .Returns(normDictionary);
        _mockNormProvider
            .Setup(provider => provider.IsInverted)
            .Returns(isInverted);

        var sdScore = _testCalculator.SdScore(value, 0, patient);

        Assert.Equal(expectedSdScore, sdScore);
    }
}