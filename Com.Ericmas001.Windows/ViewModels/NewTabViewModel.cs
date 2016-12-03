using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;

namespace Com.Ericmas001.Windows.ViewModels
{
    public abstract class NewTabViewModel : BaseTabViewModel
    {
        protected override string IconImageName => "Add";

        public abstract BaseTabViewModel CreateContentTab();

        private RelayCommand m_StartNewTabCommand;
        public ICommand StartNewTabCommand { get { return m_StartNewTabCommand ?? (m_StartNewTabCommand = new RelayCommand(() => CreateNewTab(CreateContentTab()), IsAllInputsValidated)); } }

    }
}
