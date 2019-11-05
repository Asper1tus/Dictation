namespace Dictation.Commands
{
    using System;
    using System.Windows.Input;

    public class ParametersCommand : ICommand
    {
        private readonly Action<object> execute;
        private readonly Func<bool> canExecute;

        public event EventHandler CanExecuteChanged;

        public ParametersCommand (Action<object> execute)
            : this(execute, null)
        {
        }

        public ParametersCommand (Action<object> execute, Func<bool> canExecute)
        {
            this.execute = execute ?? throw new ArgumentNullException("execute");
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute();
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}