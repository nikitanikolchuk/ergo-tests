using System.ComponentModel;
using TestAdministration.Models.Data;
using TestAdministration.Models.TestBuilders;

namespace TestAdministration.ViewModels.Testing;

/// <summary>
/// A view model for dynamic displaying of current section and
/// trial based on <see cref="ITestBuilder"/> state and patient.
/// </summary>
public class TestConductionTitleViewModel(
    ITestBuilder testBuilder,
    Hand dominantHand
) : ViewModelBase
{
    public string CurrentTitle => _getTitle();

    private string _getTitle()
    {
        var test = testBuilder.Type.ToString().ToUpper();
        var section = _getSectionName(testBuilder.CurrentSection);
        var hand = _getDominantHandString(dominantHand);
        var trial = _getTrialName(testBuilder.Type, testBuilder.CurrentTrial);

        return $"{test} – {section} (dom. {hand}): {trial}";
    }

    private static string _getSectionName(int section) => section switch
    {
        0 => "Dominantní ruka",
        1 => "Nedominantní ruka",
        2 => "Obě ruce",
        3 => "Kompletování",
        _ => throw new ArgumentOutOfRangeException(
            nameof(section),
            section,
            "Input section number not in range 0..3"
        )
    };

    private static string _getDominantHandString(Hand dominantHand) => dominantHand switch
    {
        Hand.None => throw new ArgumentException("Invalid value of dominant hand"),
        Hand.Left => "LHK",
        Hand.Right => "PHK",
        Hand.Both => throw new ArgumentException("Invalid value of dominant hand"),
        _ => throw new InvalidEnumArgumentException(
            nameof(dominantHand),
            Convert.ToInt32(dominantHand),
            typeof(Hand)
        )
    };

    private static string _getTrialName(TestType testType, int trial) => testType switch
    {
        TestType.Nhpt => _getTrialNamePractice(trial),
        TestType.Ppt => _getTrialNameSimple(trial),
        TestType.Bbt => _getTrialNamePractice(trial),
        _ => throw new InvalidEnumArgumentException(
            nameof(testType),
            Convert.ToInt32(testType),
            typeof(TestType)
        )
    };

    private static string _getTrialNamePractice(int trial) =>
        trial == 0
            ? "Zkušební pokus"
            : $"{trial}. pokus";

    private static string _getTrialNameSimple(int trial) => $"{trial + 1}. pokus";
}