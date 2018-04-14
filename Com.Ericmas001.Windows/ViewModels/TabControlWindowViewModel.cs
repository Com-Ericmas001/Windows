using System.Windows.Shell;

namespace Com.Ericmas001.Windows.ViewModels
{
    public class TabControlWindowViewModel : TabControlViewModel, IAppWindowViewModel
    {
        private readonly ITabControlWindowParms m_Parms;

        public TabControlWindowViewModel(ITabControlWindowParms parms)
        {
            m_Parms = parms;
            AppWindow.Instance = this;
        }

        private bool m_IsWindowActive;
        public bool IsWindowActive
        {
            get => m_IsWindowActive;
            set
            {
                m_IsWindowActive = value;
                if (m_IsWindowActive && ProgressState == TaskbarItemProgressState.Normal)
                    AppWindow.ShouldWindowBlink = false;
            }
        }

        public TaskbarItemProgressState ProgressState { get; set; }

        public int ProgressValue { get; set; }

        public string Title => m_Parms.AppTitle;
        protected override bool KeepNewTab => m_Parms.CacheNewTab;

        protected override NewTabViewModel CreateNewTab() => m_Parms.CreateNewTab();
    }
}
