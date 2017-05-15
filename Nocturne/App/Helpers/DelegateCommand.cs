using System;
using System.Windows.Input;

namespace Nocturne.App.Helpers
{
    public class DelegateCommand : DelegateCommand<object>
    {
        public DelegateCommand(Action<object> execute, Predicate<object> canExecute = null) : base(execute, canExecute)
        {
        }
    }

    public class DelegateCommand<T> : ICommand
    {
        readonly Action<T> _execute;
        readonly Predicate<T> _canExecute;

        // Disable unused warning.
#pragma warning disable 67
        public event EventHandler CanExecuteChanged;
#pragma warning restore 67

        public DelegateCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute != null ? _canExecute((T)parameter) : true;
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }
}
