using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using TestAdministration.Models.Data;
using TestAdministration.Models.Storages.Converters;
using Wpf.Ui.Input;
using MessageBox = Wpf.Ui.Controls.MessageBox;

namespace TestAdministration.ViewModels.Testing.Results;

/// <summary>
/// A view model for showing results of the last test.
/// </summary>
public class ResultsViewModel(
    NormInterpretationConverter normInterpretationConverter,
    DocumentationConverter documentationConverter,
    Patient patient,
    int patientAge,
    Test test,
    Test? previousTest,
    Action<Patient, Test, List<string>> onSaveTest
) : ViewModelBase
{
    private bool _isPreviousTestShown;
    private bool _isSdScoreShown;
    private bool _isNormInterpretationShown;

    public List<ResultPatientTable> Patients =>
    [
        new ResultPatientTable(
            $"{patient.Name} {patient.Surname}",
            patientAge,
            patient.DominantHand == Hand.Right ? "Pravá" : "Levá"
        )
    ];

    public List<ResultTableViewModel> Tables => _getTables();

    public bool IsPreviousTestShown
    {
        get => _isPreviousTestShown;
        set
        {
            _isPreviousTestShown = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsPreviousSdScoreShown));
            OnPropertyChanged(nameof(IsPreviousNormInterpretationShown));
        }
    }

    public bool IsSdScoreShown
    {
        get => _isSdScoreShown;
        set
        {
            _isSdScoreShown = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsPreviousSdScoreShown));
        }
    }

    public bool IsPreviousSdScoreShown => IsPreviousTestShown && IsSdScoreShown;

    public bool IsNormInterpretationShown
    {
        get => _isNormInterpretationShown;
        set
        {
            _isNormInterpretationShown = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsPreviousNormInterpretationShown));
        }
    }

    public bool IsPreviousNormInterpretationShown => IsPreviousTestShown && IsNormInterpretationShown;
    public string Notes => CsvConversionHelper.CreateNotes(test);
    public ResultFilesBoxViewModel FilesBoxViewModel { get; } = new();

    public ICommand OnGetDocumentationText => new RelayCommand<object?>(_ => _onGetDocumentationText());

    public ICommand OnSaveTest => new RelayCommand<object?>(_ =>
        onSaveTest(patient, test, FilesBoxViewModel.FilePaths.ToList())
    );

    private List<ResultTableViewModel> _getTables() => test.Type switch
    {
        TestType.Nhpt => _getNhptTables(),
        TestType.Ppt => _getPptTables(),
        TestType.Bbt => _getBbtTables(),
        _ => throw new InvalidEnumArgumentException(
            nameof(test.Type),
            Convert.ToInt32(test.Type),
            typeof(TestType)
        )
    };

    private List<ResultTableViewModel> _getNhptTables() =>
    [
        new ResultTableViewModel(
            normInterpretationConverter,
            test.Sections[0],
            previousTest?.Sections[0],
            "Dominantní HK",
            "Čas (v sekundách)"
        ),
        new ResultTableViewModel(
            normInterpretationConverter,
            test.Sections[1],
            previousTest?.Sections[1],
            "Nedominantní HK",
            "Čas (v sekundách)"
        )
    ];

    private List<ResultTableViewModel> _getPptTables() =>
    [
        new ResultTableViewModel(
            normInterpretationConverter,
            test.Sections[0],
            previousTest?.Sections[0],
            "Dominantní HK",
            "Počet kolíků"
        ),
        new ResultTableViewModel(
            normInterpretationConverter,
            test.Sections[1],
            previousTest?.Sections[1],
            "Nedominantní HK",
            "Počet kolíků"
        ),
        new ResultTableViewModel(
            normInterpretationConverter,
            test.Sections[2],
            previousTest?.Sections[2],
            "Obě HK",
            "Počet párů kolíků"
        ),
        new ResultTableViewModel(
            normInterpretationConverter,
            test.Sections[3],
            previousTest?.Sections[3],
            "LHK + PHK + Obě",
            "Součet výsledků"
        ),
        new ResultTableViewModel(
            normInterpretationConverter,
            test.Sections[4],
            previousTest?.Sections[4],
            "Kompletování",
            "Počet součástek"
        )
    ];

    private List<ResultTableViewModel> _getBbtTables() =>
    [
        new ResultTableViewModel(
            normInterpretationConverter,
            test.Sections[0],
            previousTest?.Sections[0],
            "Dominantní HK",
            "Počet kostek"
        ),
        new ResultTableViewModel(
            normInterpretationConverter,
            test.Sections[1],
            previousTest?.Sections[1],
            "Nedominantní HK",
            "Počet kostek"
        )
    ];

    private async void _onGetDocumentationText()
    {
        var text = documentationConverter.Convert(test);
        Clipboard.SetDataObject(text);

        var messageBox = new MessageBox
        {
            Title = "Informace",
            Content = "Text do dokumentace byl zkopírován",
            CloseButtonText = "Zavřít"
        };

        await messageBox.ShowDialogAsync();
    }
}