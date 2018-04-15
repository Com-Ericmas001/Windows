using System;
using System.Windows.Shell;
using Com.Ericmas001.DependencyInjection;
using Com.Ericmas001.Windows.Services;

namespace Com.Ericmas001.Windows.ViewModels
{
    public abstract class BaseMainWindowViewModel : TabControlViewModel, IAppWindowViewModel
    {
        private class DumbResolverService : IResolverService
        {
            public T Resolve<T>()
            {
                throw new NotImplementedException();
            }

            public object Resolve(Type t)
            {
                throw new NotImplementedException();
            }
        }
        protected BaseMainWindowViewModel() : base(new TabFactoryService(new DumbResolverService()))
        {
            AppWindow.Instance = this;
            AddNewTab();
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

        public abstract string Title { get; }
    }
}
