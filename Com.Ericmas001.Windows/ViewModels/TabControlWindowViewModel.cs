using System.Windows.Shell;
using Com.Ericmas001.Windows.Services.Interfaces;

namespace Com.Ericmas001.Windows.ViewModels
{
    public class TabControlWindowViewModel : TabControlViewModel, IAppWindowViewModel
    {
        private readonly ITabControlAppParms m_Parms;
        private readonly ITabFactoryService m_TabFactory;

        public TabControlWindowViewModel(ITabControlAppParms parms, ITabFactoryService tabFactory) : base(tabFactory)
        {
            m_Parms = parms;
            m_TabFactory = tabFactory;
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

        protected override NewTabViewModel CreateNewTab() => m_TabFactory.CreateNewTab();
    }
}
