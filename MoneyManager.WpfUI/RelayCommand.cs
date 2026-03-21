using System;
using System.Windows.Input;

namespace MoneyManager.WpfUI.ViewModels
{
    public class RelayCommand : ICommand
    {
        private Action<object?> execute;

        public RelayCommand(Action<object?> executeAction)
        {
            execute = executeAction;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            execute(parameter);
        }
    }
}
