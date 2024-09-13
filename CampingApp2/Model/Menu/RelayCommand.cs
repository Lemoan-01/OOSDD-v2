using System;
using System.Windows.Input;

namespace ViewModels
{
    // This class helps create reusable commands for UI actions.
    public class RelayCommand : ICommand
    {
        // The action to perform when the command is executed.
        private readonly Action<object> _execute;

        // A condition to check if the command can be executed at the moment.
        private readonly Func<object, bool> _canExecute;

        // This event notifies when the command's ability to execute changes.
        public event EventHandler CanExecuteChanged
        {
            // When a handler is added, it listens for changes in command executability.
            add { CommandManager.RequerySuggested += value; }

            // When a handler is removed, it stops listening for changes in command executability.
            remove { CommandManager.RequerySuggested -= value; }
        }

        // Constructs a new RelayCommand with the provided action and condition.
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        // Checks if the command can be executed right now.
        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

        // Executes the command's action.
        public void Execute(object parameter) => _execute(parameter);
    }
}
