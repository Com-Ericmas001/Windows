using System;
using System.ComponentModel;
using System.Windows.Threading;
using Com.Ericmas001.Common;

namespace Com.Ericmas001.Windows.ViewModels
{
    public delegate void EmptyHandler();
    public class LoadingViewModel : BaseViewModel
    {
        public event EmptyHandler OnDataObtained = delegate { };
        public event EventHandler<EventArgs<Exception>> OnErrorObtained = delegate { };

        private readonly Dispatcher m_AppCurrentDispatcher;
        private bool m_IsLoading;
        private readonly BackgroundWorker m_Bw = new BackgroundWorker();
        private string m_BigLoadingMessage = "Loading Data ...";
        private string m_SmallLoadingMessage = "Fetching data from Database ...";

        public bool IsLoading
        {
            get { return m_IsLoading; }
            set
            {
                if ((m_IsLoading != value))
                    Set(ref m_IsLoading, value);
            }
        }

        public string BigLoadingMessage
        {
            get { return m_BigLoadingMessage; }
            set { Set(ref m_BigLoadingMessage, value); }
        }

        public string SmallLoadingMessage
        {
            get { return m_SmallLoadingMessage; }
            set { Set(ref m_SmallLoadingMessage, value); }
        }
        public LoadingViewModel( Dispatcher appCurrentDispatcher, DoWorkEventHandler fnctObtainData )
        {
            m_AppCurrentDispatcher = appCurrentDispatcher;
            m_Bw.DoWork += fnctObtainData;
            m_Bw.RunWorkerCompleted += DataObtained;
        }

        private void DataObtained(object sender, RunWorkerCompletedEventArgs e)
        {
            IsLoading = false;
            if (e.Error != null)
                OnErrorObtained(this, new EventArgs<Exception>(e.Error));
            RefreshInterfaceAsync();
        }


        private void RefreshInterfaceAsync()
        {
            m_AppCurrentDispatcher.Invoke(RefreshInterface);
        }

        private void RefreshInterface()
        {
            OnDataObtained();
        }

        public void Execute()
        {
            if (!m_Bw.IsBusy)
            {
                IsLoading = true;
                m_Bw.RunWorkerAsync();
            }
        }
    }
}
