using System.Diagnostics;
using System.Windows.Input;

namespace TestAdministration.ViewModels;

/// <summary>
/// Basic implementation of <c>ICommand</c>.
/// </summary>
public class RelayCommand(
    Action<object?> execute,
    Predicate<object?>? canExecute = null
) : ICommand
{
    [DebuggerStepThrough]
    public bool CanExecute(object? parameter)
    {
        return canExecute?.Invoke(parameter) ?? true;
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public void Execute(object? parameter)
    {
        execute(parameter);
    }
}