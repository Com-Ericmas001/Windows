﻿using System.Windows.Shell;

namespace Com.Ericmas001.Windows.ViewModels
{
    public abstract class BaseMainWindowViewModel : TabControlViewModel, IAppWindowViewModel
    {

        protected BaseMainWindowViewModel()
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