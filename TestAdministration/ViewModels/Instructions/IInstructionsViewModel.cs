using System.Runtime.CompilerServices;

namespace TestAdministration.ViewModels.Instructions;

public interface IInstructionsViewModel
{
    ViewModelBase CurrentViewModel { get; }
    void OnPropertyChanged([CallerMemberName] string? propertyName = null);
}