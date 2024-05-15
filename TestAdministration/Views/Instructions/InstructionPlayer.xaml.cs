using System.Windows;
using TestAdministration.ViewModels.Instructions;

namespace TestAdministration.Views.Instructions;

public partial class InstructionPlayer
{
    public InstructionPlayer()
    {
        InitializeComponent();
        DataContextChanged += _onDataContextChanged;
    }

    private void _onDataContextChanged(object _, DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue is InstructionPlayerViewModel viewModel)
        {
            viewModel.OnPlayStateChanged += BringIntoView;
        }
    }
}