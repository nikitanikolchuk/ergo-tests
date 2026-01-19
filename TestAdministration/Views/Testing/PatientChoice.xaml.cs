using TestAdministration.ViewModels.Testing;
using Wpf.Ui.Controls;

namespace TestAdministration.Views.Testing;

public partial class PatientChoice
{
    public PatientChoice()
    {
        InitializeComponent();

        Loaded += (_, _) => _wireDialog();
        DataContextChanged += (_, _) => _wireDialog();
    }

    private void _wireDialog()
    {
        if (Resources["TrialCountDialog"] is not ContentDialog dialog ||
            DataContext is not PatientChoiceViewModel viewModel)
        {
            return;
        }

        viewModel.TrialCountDialog = dialog;
        dialog.DataContext = viewModel;
    }
}