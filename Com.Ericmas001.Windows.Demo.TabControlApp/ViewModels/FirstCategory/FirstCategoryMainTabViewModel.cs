using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Com.Ericmas001.Windows.Demo.TabControlApp.ViewModels.FirstCategory.Models;
using Com.Ericmas001.Windows.ViewModels;
using Com.Ericmas001.Windows.Xaml.Windows;
using GalaSoft.MvvmLight.CommandWpf;

namespace Com.Ericmas001.Windows.Demo.TabControlApp.ViewModels.FirstCategory
{
    public class FirstCategoryMainTabViewModel : BaseContentViewModel
    {
        private const string LOADING_IMAGE = "Loading";
        private const string NORMAL_IMAGE = "First";
        public FirstCategoryParms Parms { get; }
        private bool? m_Success;
        public override string BigLoadingMessage => "Get Stuff";
        public override string SmallLoadingMessage => "Getting all the stuff...";

        public string SomeBool => Parms.SomeBool.ToString().ToUpper();
        public override string TabHeader { get; } = "First " + DateTime.Now.ToString("HH:mm:ss");
        public override bool CanCloseTab => true;
        private string m_IconImageName = NORMAL_IMAGE;
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

        private RelayCommand<Size> m_NewWindowCommand;
        public ICommand NewWindowCommand => m_NewWindowCommand ?? (m_NewWindowCommand = new RelayCommand<Size>(CreateNewWindow));
        public bool CanCreateNewWindow { get; set; }
        private void CreateNewWindow(Size s)
        {
            var window = new MainTabWindow(new MainTabWindowViewModel(new FirstCategoryMainTabViewModel(this), TabHeader, s, NORMAL_IMAGE));
            window.Show();
        }
        public FirstCategoryMainTabViewModel(FirstCategoryParms parms)
            : base(Application.Current.Dispatcher)
        {
            CanCreateNewWindow = true;
            Parms = parms;
            RefreshDataAndInterface();
        }
        public FirstCategoryMainTabViewModel(FirstCategoryMainTabViewModel vmToClone)
            : base(Application.Current.Dispatcher)
        {
            CanCreateNewWindow = false;
            Parms = vmToClone.Parms;
            Success = vmToClone.Success;
        }

        protected override void ObtainData(object sender, DoWorkEventArgs e)
        {
            Success = null;
            try
            {
                TabIconName = LOADING_IMAGE;
                Thread.Sleep(2000);
                Success = true;
            }
            catch (Exception)
            {
                Success = false;
            }
            finally
            {
                TabIconName = NORMAL_IMAGE;
                AppWindow.ShouldWindowBlink = true;
            }
        }

        protected override void RefreshInterface()
        {

        }
    }
}
