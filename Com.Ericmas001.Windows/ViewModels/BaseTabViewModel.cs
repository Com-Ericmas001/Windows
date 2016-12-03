﻿using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using GalaSoft.MvvmLight.CommandWpf;

namespace Com.Ericmas001.Windows.ViewModels
{
    public delegate void NewTabEventHandler(object sender, BaseTabViewModel tab);
    public class BaseTabViewModel : BaseViewModel
    {
        public event NewTabEventHandler OnTabCreation = delegate { };

        private ICommand m_CloseTabCommand;

        public virtual string TabHeader => null;

        public virtual string TabTitle => null;

        protected virtual string IconImageName => null;

        public virtual ImageSource TabIcon => string.IsNullOrEmpty(IconImageName) ? null : Application.Current.FindResource(IconImageName) as ImageSource;

        protected virtual string IconBigImageName => null;

        public virtual ImageSource TabIconBig => string.IsNullOrEmpty(IconBigImageName) ? null : Application.Current.FindResource(IconBigImageName) as ImageSource;

        public virtual bool CanCloseTab => false;

        public string Title => !string.IsNullOrEmpty(TabTitle) ? TabTitle : TabHeader;

        public bool HasHeaderText => !string.IsNullOrEmpty(TabHeader);
        public bool HasHeaderIcon => TabIcon != null;

        public bool HasTitle => !string.IsNullOrEmpty(Title);
        public bool HasTitleImage => !string.IsNullOrEmpty(IconBigImageName);

        public ICommand CloseTabCommand
        {
            get
            {
                if (m_CloseTabCommand == null)
                    m_CloseTabCommand = new RelayCommand(CloseView, () => CanCloseTab);
                return m_CloseTabCommand;
            }
        }

        public void CreateNewTab(BaseTabViewModel tab)
        {
            OnTabCreation(this, tab);
        }
    }
}