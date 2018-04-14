using System.Windows.Shell;

namespace Com.Ericmas001.Windows.ViewModels
{
    public abstract class BaseMainWindowViewModel : TabControlViewModel
    {
        private static BaseMainWindowViewModel m_Instance;

        protected BaseMainWindowViewModel()
        {
            m_Instance = this;
        }
        public static bool ShouldWindowBlink
        {
            set
            {
                bool shouldBlink = value && !m_Instance.IsWindowActive;
                m_Instance.ProgressState = shouldBlink ? TaskbarItemProgressState.Normal : TaskbarItemProgressState.None;
                m_Instance.ProgressValue = shouldBlink ? 100 : 0;
                m_Instance.RaisePropertyChanged(nameof(ProgressState));
                m_Instance.RaisePropertyChanged(nameof(ProgressValue));
            }
        }

        private bool m_IsWindowActive;
        public bool IsWindowActive
        {
            get => m_IsWindowActive;
            set
            {
                m_IsWindowActive = value;
                if (m_IsWindowActive && ProgressState == TaskbarItemProgressState.Normal)
                    ShouldWindowBlink = false;
            }
        }

        public TaskbarItemProgressState ProgressState { get; set; }

        public int ProgressValue { get; set; }

        public abstract string Title { get; }
    }
}
