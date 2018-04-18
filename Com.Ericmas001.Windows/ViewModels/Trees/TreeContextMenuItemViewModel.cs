using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using GalaSoft.MvvmLight.CommandWpf;

namespace Com.Ericmas001.Windows.ViewModels.Trees
{
    public abstract class TreeContextMenuItemViewModel : BaseViewModel
    {
        protected TreeElementViewModel Element { get; private set; }

        public abstract string Text { get; }

        public virtual ImageSource Icon => string.IsNullOrEmpty(IconImageName) ? null : Application.Current.FindResource(IconImageName) as ImageSource;
        public virtual string IconImageName { get { return null; } }

        private RelayCommand m_ExecuteCommand;
        public ICommand ExecuteCommand { get { return m_ExecuteCommand ?? (m_ExecuteCommand = new RelayCommand(Execute, CanExecute)); } }

        public TreeContextMenuItemViewModel(TreeElementViewModel element)
        {
            Element = element;
        }

        protected abstract void Execute();

        protected abstract bool CanExecute();
    }
}
