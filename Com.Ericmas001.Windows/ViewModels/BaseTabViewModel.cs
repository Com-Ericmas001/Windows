using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using GalaSoft.MvvmLight.CommandWpf;

namespace Com.Ericmas001.Windows.ViewModels
{
    public delegate void NewTabEventHandler(object sender, BaseTabViewModel tab);
    public delegate void CreateNewTabEventHandler(object sender, Type vmType, object parms);
    public class BaseTabViewModel : BaseViewModel
    {
        public event NewTabEventHandler OnTabCreation = delegate { };
        public event CreateNewTabEventHandler OnCreateNewTab = delegate { };
        public event EventHandler<BaseTabViewModel> OnAttachDetachWindow = delegate { };

        private RelayCommand m_AttachDetachWindowCommand;
        public ICommand AttachDetachWindowCommand => m_AttachDetachWindowCommand ?? (m_AttachDetachWindowCommand = new RelayCommand(() =>
        {
            OnAttachDetachWindow(this, this);
        }));

        private ICommand m_CloseTabCommand;

        public virtual string TabHeader => null;

        public virtual string TabTitle => null;

        protected virtual string IconImageName => null;

        public virtual ImageSource TabIcon => string.IsNullOrEmpty(IconImageName) ? null : Application.Current.FindResource(IconImageName) as ImageSource;

        protected virtual string IconBigImageName => null;

        public virtual ImageSource TabIconBig => string.IsNullOrEmpty(IconBigImageName) ? null : Application.Current.FindResource(IconBigImageName) as ImageSource;

        public virtual bool CanCloseTab => false;

        public string Title => !string.IsNullOrEmpty(TabTitle) ? TabTitle : TabHeader;

        public bool HasHeaderText => !string.IsNullOrEmpty(TabHeader);
        public bool HasHeaderIcon => TabIcon != null;

        public bool HasTitle => !string.IsNullOrEmpty(Title);
        public bool HasTitleImage => !string.IsNullOrEmpty(IconBigImageName);

        public ICommand CloseTabCommand
        {
            get
            {
                if (m_CloseTabCommand == null)
                    m_CloseTabCommand = new RelayCommand(CloseView, () => CanCloseTab);
                return m_CloseTabCommand;
            }
        }

        public void CreateNewTab(BaseTabViewModel tab)
        {
            OnTabCreation(this, tab);
        }
        public void CreateNewTab<TVm, TParms>(TParms parms) where TVm : IMainTabViewModel<TParms>
        {
            OnCreateNewTab(this, typeof(TVm), parms);
        }
        public void CreateNewTab(Type vmType, object parms)
        {
            OnCreateNewTab(this, vmType, parms);
        }
    }
}
