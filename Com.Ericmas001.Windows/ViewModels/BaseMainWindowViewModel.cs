using System;
using System.Reflection;
using System.Windows.Shell;
using Com.Ericmas001.DependencyInjection.Resolvers.Interfaces;
using Com.Ericmas001.Windows.Services;

namespace Com.Ericmas001.Windows.ViewModels
{
    public abstract class BaseMainWindowViewModel : TabControlViewModel, IAppWindowViewModel
    {
        private class DumbResolverService : IResolverService
        {
            public T Resolve<T>()
            {
                return (T)Resolve(typeof(T));
            }

            public object Resolve(Type t)
            {
                ConstructorInfo ctor = t.GetConstructor(Type.EmptyTypes);
                return ctor?.Invoke(new object[0]);
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
