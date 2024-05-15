namespace TestAdministration.ViewModels.Testing.Instructions;

/// <summary>
/// An interface for defining the first audio instruction to be
/// played.
/// </summary>
public interface IInstructionsPageViewModel
{
    InstructionPlayerViewModel FirstAudioInstructionViewModel { get; }
}