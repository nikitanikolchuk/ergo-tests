using Moq;
using TestAdministration.Models.Data;
using TestAdministration.Models.Services;
using TestAdministration.Models.Utils;

namespace TestAdministration.Tests.Models.Services;

public class AgeCalculatorServiceTests
{
    private readonly Mock<IDateTimeProvider> _mockDateTimeProvider;
    private readonly AgeCalculatorService _ageCalculatorService;

    public AgeCalculatorServiceTests()
    {
        _mockDateTimeProvider = new Mock<IDateTimeProvider>();
        _ageCalculatorService = new AgeCalculatorService(_mockDateTimeProvider.Object);
    }

    [Theory]
    [InlineData("2000-01-01", "2021-07-01", 21)]
    [InlineData("2000-06-30", "2021-07-01", 21)]
    [InlineData("2000-07-01", "2021-07-01", 21)]
    [InlineData("2000-07-02", "2021-07-01", 20)]
    [InlineData("2001-01-01", "2021-07-01", 20)]
    public void Calculate_ReturnsCorrectAge(DateTime birthDate, DateTime today, int expectedAge)
    {
        var patient = new Patient(
            "Id",
            "Name",
            "Surname",
            true,
            DateOnly.FromDateTime(birthDate),
            Hand.Right,
            Hand.Right
        );

        _mockDateTimeProvider
            .Setup(provider => provider.Today)
            .Returns(DateOnly.FromDateTime(today));

        var age = _ageCalculatorService.Calculate(patient);

        Assert.Equal(expectedAge, age);
    }
}