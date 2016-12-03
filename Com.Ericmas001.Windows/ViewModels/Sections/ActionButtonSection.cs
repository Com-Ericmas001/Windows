using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;

namespace Com.Ericmas001.Windows.ViewModels.Sections
{
    public abstract class ActionButtonSection : TabSection
    {
        private RelayCommand m_ExecuteCommand;
        public ICommand ExecuteCommand
        {
            get { return m_ExecuteCommand ?? (m_ExecuteCommand = new RelayCommand(() => CreateNewTab(CreateContentTab()))); }
        }
    }
}
