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
        private readonly Dispatcher m_AppCurrentDispatcher;
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

        public LoadingViewModel LoadingDataVm { get; private set; }

        public virtual string BigLoadingMessage => "Loading ...";

        public virtual string SmallLoadingMessage => "Fetching data ...";

        public virtual bool CanRefresh => true;

        private RelayCommand m_RefreshCommand;
        public ICommand RefreshCommand
        {
            get { return m_RefreshCommand ?? (m_RefreshCommand = new RelayCommand(RefreshDataAndInterface, () => CanRefresh)); }
        }

        protected BaseContentViewModel( Dispatcher appCurrentDispatcher )
        {
            m_AppCurrentDispatcher = appCurrentDispatcher;
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

        public override void OnLoadFinished()
        {
            LoadingDataVm = new LoadingViewModel(m_AppCurrentDispatcher, ObtainDataLocal)
            {
                BigLoadingMessage = BigLoadingMessage,
                SmallLoadingMessage = SmallLoadingMessage
            };
            LoadingDataVm.OnDataObtained += RefreshInterface;
            LoadingDataVm.OnErrorObtained += m_LoadingDataVm_OnErrorObtained;
            RefreshDataAndInterface();
        }
    }
}
