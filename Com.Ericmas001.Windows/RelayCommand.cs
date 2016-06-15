using System;
using System.Windows.Input;

namespace Com.Ericmas001.Windows
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> m_Execute;
        private readonly Predicate<T> m_CanExecute;
        
        #region "Constructors"

        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            m_Execute = execute;
            m_CanExecute = canExecute;
        }

        #endregion

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            return m_CanExecute == null || m_CanExecute((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested += value; }
        }

        public void Execute(object parameter)
        {
            m_Execute((T)parameter);
        }
        #endregion ICommand Members
    }
    public class RelayCommand : RelayCommand<object>
    {
        public RelayCommand(Action<object> execute)
            : base(execute, null)
        {
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
            : base(execute, canExecute)
        {
        }
    }
}
