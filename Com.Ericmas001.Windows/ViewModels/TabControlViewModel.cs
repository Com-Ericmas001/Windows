using System;
using System.Collections.ObjectModel;

namespace Com.Ericmas001.Windows.ViewModels
{
    public abstract class TabControlViewModel: BaseViewModel, ITabCreationViewModel
    {
        protected virtual NewTabViewModel CreateNewTab() 
        { 
            return null; 
        }
        protected virtual bool KeepNewTab => false;

        public ObservableCollection<BaseTabViewModel> Tabs { get; } = new ObservableCollection<BaseTabViewModel>();
        private NewTabViewModel m_NewTab;

        private BaseTabViewModel m_SelectedTab;
        public BaseTabViewModel SelectedTab
        {
            get { return m_SelectedTab; }
            set { Set(ref m_SelectedTab, value); }
        }

        public void AddTab(BaseTabViewModel tab)
        {
            if (tab != null)
            {
                tab.OnTabCreation += (s, t) => AddTab(t);
                tab.OnRequestClose += OnTabClosed;
                Tabs.Insert(Tabs.Count-1,tab);
                SelectedTab = tab;

                if (!KeepNewTab)
                    AddNewTab();
            }
        }

        public void AddNewTab()
        {
            if (m_NewTab != null)
                Tabs.Remove(m_NewTab);
            bool mustAdd = (!KeepNewTab || m_NewTab == null);
            if (mustAdd)
                m_NewTab = CreateNewTab();

            if (m_NewTab != null)
            {
                if (mustAdd)
                    m_NewTab.OnTabCreation += (s, t) => AddTab(t);
                Tabs.Add(m_NewTab);
            }
        }
        public void OnTabClosed(object sender, EventArgs e)
        {
            var tab = sender as BaseTabViewModel;
            if (tab == null)
                return;
            tab.OnRequestClose -= OnTabClosed;
            Tabs.Remove(tab);
        }
        public void SelectNewTab()
        {
            SelectedTab = m_NewTab;
        }
    }
}
