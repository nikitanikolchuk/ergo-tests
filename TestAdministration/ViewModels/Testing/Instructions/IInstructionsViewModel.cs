using System.Runtime.CompilerServices;

namespace TestAdministration.ViewModels.Testing.Instructions;

public interface IInstructionsViewModel
{
    ViewModelBase CurrentViewModel { get; }
    void OnPropertyChanged([CallerMemberName] string? propertyName = null);
}