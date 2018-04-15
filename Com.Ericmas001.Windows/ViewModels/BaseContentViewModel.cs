using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Threading;
using Com.Ericmas001.Common;
using GalaSoft.MvvmLight.CommandWpf;

namespace Com.Ericmas001.Windows.ViewModels
{
    public abstract class BaseContentViewModel : BaseTabViewModel
    {
        private readonly LoadingViewModel m_LoadingDataVm;
        private bool? m_Success;
        protected abstract string NormalIconImageName { get; }
        private string m_IconImageName;
        protected override string IconImageName => TabIconName;
        public bool IsSuccess => m_Success != null && m_Success.Value;
        public bool IsError => m_Success != null && !m_Success.Value;
        private string TabIconName
        {
            get => m_IconImageName;
            set
            {
                Set(ref m_IconImageName, value);
                RaisePropertyChanged(nameof(IconImageName));
                RaisePropertyChanged(nameof(TabIcon));
            }
        }
        public bool? Success
        {
            get => m_Success;
            set
            {
                Set(ref m_Success, value);
                RaisePropertyChanged(nameof(IsSuccess));
                RaisePropertyChanged(nameof(IsError));
            }
        }

        public LoadingViewModel LoadingDataVm
        {
            get { return m_LoadingDataVm; }
        } 

        public virtual string BigLoadingMessage
        {
            get { return "Loading Data ..."; }
        }
        public virtual string SmallLoadingMessage
        {
            get { return "Fetching data from Database ..."; }
        }

        public virtual bool CanRefresh
        {
            get { return true; }
        }

        private RelayCommand m_RefreshCommand;
        public ICommand RefreshCommand
        {
            get { return m_RefreshCommand ?? (m_RefreshCommand = new RelayCommand(RefreshDataAndInterface, () => CanRefresh)); }
        }

        public BaseContentViewModel( Dispatcher appCurrentDispatcher )
        {
            m_LoadingDataVm = new LoadingViewModel(appCurrentDispatcher, ObtainDataLocal)
            {
                BigLoadingMessage = BigLoadingMessage,
                SmallLoadingMessage = SmallLoadingMessage
            };
            m_LoadingDataVm.OnDataObtained += RefreshInterface;
            m_LoadingDataVm.OnErrorObtained += m_LoadingDataVm_OnErrorObtained;
        }

        void m_LoadingDataVm_OnErrorObtained(object sender, EventArgs<Exception> e)
        {
            Logs.LogError(e.Info.ToString());
        }

        public virtual void Init()
        {
            
        }

        protected virtual void RefreshInterface()
        {

        }

        private void ObtainDataLocal(object sender, DoWorkEventArgs e)
        {
            Success = null;
            TabIconName = "Loading";
            try
            {
                Success = ObtainData(sender, e);
            }
            catch (Exception)
            {
                Success = false;
            }
            finally
            {
                TabIconName = NormalIconImageName;
                AppWindow.ShouldWindowBlink = true;
            }
        }
        protected abstract bool ObtainData(object sender, DoWorkEventArgs e);

        protected void RefreshDataAndInterface()
        {
            LoadingDataVm.Execute();
        }
    }
}
