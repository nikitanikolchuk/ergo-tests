using System.ComponentModel;
using System.Text.RegularExpressions;
using TestAdministration.Models.Data;
using TestAdministration.Models.Storages;
using Wpf.Ui.Controls;

namespace TestAdministration.ViewModels;

/// <summary>
/// A view model for saving patients' personal data and its
/// validation.
/// </summary>
public partial class NewPatientViewModel(
    ITestStorage testStorage
) : ViewModelBase
{
    private const string Male = "Muž";
    private const string Female = "Žena";
    private const string LeftHand = "Levá";
    private const string RightHand = "Pravá";
    private const string BothHands = "Obě";

    // Characters that cannot be used in a directory name.
    private static readonly List<char> ForbiddenIdChars = ['/', '\\', ':', '*', '?', '"', '<', '>', '|'];

    [GeneratedRegex(" +")]
    private static partial Regex MultipleSpaceRegex();

    private string _name = string.Empty;
    private string _surname = string.Empty;
    private string _id = string.Empty;
    private bool? _isMale;
    private DateOnly? _birthDate;
    private Hand? _dominantHand;
    private Hand? _pathologicalHand;

    public static List<string> GenderVariants => [Male, Female];
    public static List<string> DominantHandVariants => [LeftHand, RightHand];
    public static List<string> PathologicalHandVariants => [LeftHand, RightHand, BothHands];
    public Func<bool> AddPatient => _addPatient;

    public string Name
    {
        get => _name;
        set
        {
            if (value.Any(ch => !char.IsLetter(ch) && ch != ' '))
            {
                return;
            }

            _name = MultipleSpaceRegex().Replace(value, " ").TrimStart();
            OnPropertyChanged();
        }
    }

    public string Surname
    {
        get => _surname;
        set
        {
            if (value.Any(ch => !char.IsLetter(ch) && ch != ' '))
            {
                return;
            }

            _surname = MultipleSpaceRegex().Replace(value, " ").TrimStart();
            OnPropertyChanged();
        }
    }

    public string Id
    {
        get => _id;
        set
        {
            if (value.Any(ch => ForbiddenIdChars.Contains(ch)))
            {
                return;
            }

            _id = value.TrimStart();
            OnPropertyChanged();
        }
    }

    public string Gender
    {
        get
        {
            if (_isMale is null)
            {
                return string.Empty;
            }

            return _isMale.Value ? Male : Female;
        }
        set
        {
            _isMale = value switch
            {
                Male => true,
                Female => false,
                _ => null
            };
            OnPropertyChanged();
        }
    }

    public DateTime? BirthDate
    {
        get => _birthDate?.ToDateTime(TimeOnly.MinValue);
        set
        {
            if (value is null)
            {
                return;
            }

            _birthDate = DateOnly.FromDateTime(value.Value);
            OnPropertyChanged();
        }
    }

    public string DominantHand
    {
        get => _dominantHand is null ? string.Empty : _handString(_dominantHand.Value);
        set
        {
            _dominantHand = value switch
            {
                LeftHand => Hand.Left,
                RightHand => Hand.Right,
                BothHands => throw new ArgumentException("Patient can't have both dominant hands"),
                _ => throw new InvalidEnumArgumentException(
                    nameof(_dominantHand),
                    Convert.ToInt32(_dominantHand ?? 0),
                    typeof(TestType)
                )
            };
        }
    }

    public string PathologicalHand
    {
        get => _pathologicalHand is null ? string.Empty : _handString(_pathologicalHand.Value);
        set
        {
            _pathologicalHand = value switch
            {
                LeftHand => Hand.Left,
                RightHand => Hand.Right,
                BothHands => Hand.Both,
                _ => throw new InvalidEnumArgumentException(
                    nameof(_dominantHand),
                    Convert.ToInt32(_pathologicalHand ?? 0),
                    typeof(TestType)
                )
            };
        }
    }

    private string _handString(Hand hand) => hand switch
    {
        Hand.Left => LeftHand,
        Hand.Right => RightHand,
        Hand.Both => BothHands,
        _ => throw new InvalidEnumArgumentException(
            nameof(_dominantHand),
            Convert.ToInt32(_dominantHand ?? 0),
            typeof(TestType)
        )
    };

    /// <summary>
    /// Exports patient's personal data and returns true if all required values were set.
    /// </summary>
    private bool _addPatient()
    {
        if (string.IsNullOrWhiteSpace(_name))
        {
            _alertMissingParameter("Jméno");
            return false;
        }

        if (string.IsNullOrWhiteSpace(_surname))
        {
            _alertMissingParameter("Příjmení");
            return false;
        }

        if (string.IsNullOrWhiteSpace(_id))
        {
            _alertMissingParameter("Rodné číslo");
            return false;
        }

        if (_isMale is null)
        {
            _alertMissingParameter("Pohlaví");
            return false;
        }

        if (_birthDate is null)
        {
            _alertMissingParameter("Datum narození");
            return false;
        }

        if (_dominantHand is null)
        {
            _alertMissingParameter("Dominantní HK");
            return false;
        }

        if (_pathologicalHand is null)
        {
            _alertMissingParameter("HK s patologií");
            return false;
        }

        var patient = new Patient(
            _id.TrimEnd(),
            _name.TrimEnd(),
            _surname.TrimEnd(),
            _isMale.Value,
            _birthDate.Value,
            _dominantHand.Value,
            _pathologicalHand.Value
        );

        testStorage.AddPatient(patient);
        return true;
    }

    private static async void _alertMissingParameter(string parameterName)
    {
        var messageBox = new MessageBox
        {
            Title = "Chyba",
            Content = $"Nevyplnil(a) jste položku: {parameterName}",
            CloseButtonText = "Zavřít"
        };

        await messageBox.ShowDialogAsync();
    }
}