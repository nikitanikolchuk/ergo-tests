using System.Windows.Input;
using TestAdministration.Models.Data;
using TestAdministration.Models.Storages;
using Wpf.Ui;
using Wpf.Ui.Controls;
using Wpf.Ui.Input;

namespace TestAdministration.ViewModels.Testing;

/// <summary>
/// A view model for choosing a patient from a dynamic list. Also includes a
/// content dialog for choosing the number of tries before starting a test.
/// </summary>
public class PatientChoiceViewModel : ViewModelBase
{
    private const int DefaultTrialCount = 3;

    private static readonly Patient AnonymousPatient = new(
        "0",
        "Anonym",
        "Anonym",
        true,
        new DateOnly(1970, 1, 1),
        Hand.Right,
        Hand.Right
    );

    private readonly IContentDialogService _contentDialogService;
    private readonly ITestStorage _testStorage;
    private readonly List<PatientDirectoryInfo> _patients;
    private readonly Action<Patient, int> _onStartTesting;
    private readonly Action _onOpenAddPatient;

    private bool _isChoosing;

    public ContentDialog? TrialCountDialog { get; set; }

    /// <summary>
    /// Sets dependencies and adds an anonymous patient if
    /// patient list is empty.
    /// </summary>
    public PatientChoiceViewModel(
        IContentDialogService contentDialogService,
        ITestStorage testStorage,
        Action<Patient, int> onStartTesting,
        Action onOpenAddPatient
    )
    {
        _contentDialogService = contentDialogService;
        _testStorage = testStorage;
        _onStartTesting = onStartTesting;
        _onOpenAddPatient = onOpenAddPatient;
        _patients = _testStorage.GetAllPatientDirectoryInfos();
        if (_patients.Count != 0)
        {
            return;
        }

        _testStorage.AddPatient(AnonymousPatient);
        _patients = _testStorage.GetAllPatientDirectoryInfos();
    }

    public List<PatientDirectoryInfo> Patients => _patients.Where(patient =>
        patient.Surname.StartsWith(SearchText, StringComparison.CurrentCultureIgnoreCase)
    ).ToList();

    public string SearchText
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(Patients));
        }
    } = string.Empty;

    public PatientDirectoryInfo? SelectedPatient
    {
        get;
        set
        {
            if (value == field)
            {
                return;
            }

            field = value;
            OnPropertyChanged();

            if (value is null || _isChoosing)
            {
                return;
            }

            _ = _handleSelectPatient(value);
        }
    }

    public int SelectedTrialCount
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    } = DefaultTrialCount;

    public ICommand OnOpenAddPatient => new RelayCommand<object?>(_ => _onOpenAddPatient());

    private async Task _handleSelectPatient(PatientDirectoryInfo patientDirectoryInfo)
    {
        _isChoosing = true;

        try
        {
            var patient = _testStorage.GetPatient(patientDirectoryInfo);
            if (patient is null)
            {
                await _alertInvalidPatientFile();
                return;
            }

            var trialCount = await _showTrialCountDialog();
            if (trialCount is null)
            {
                return;
            }

            _onStartTesting(patient, trialCount.Value);
        }
        finally
        {
            SelectedPatient = null;
            _isChoosing = false;
        }
    }

    private async Task<int?> _showTrialCountDialog()
    {
        if (TrialCountDialog is null)
        {
            return null;
        }

        SelectedTrialCount = DefaultTrialCount;

        var result = await _contentDialogService.ShowAsync(TrialCountDialog, CancellationToken.None);

        return result == ContentDialogResult.Primary
            ? SelectedTrialCount
            : null;
    }

    private static async Task _alertInvalidPatientFile()
    {
        var messageBox = new MessageBox
        {
            Title = "Chyba",
            Content = "Nesprávný formát souboru s osobními údaji pacienta nebo chybějící soubor. " +
                      "Otevřete návod k použití pro více informace",
            CloseButtonText = "Zavřít"
        };

        await messageBox.ShowDialogAsync();
    }
}