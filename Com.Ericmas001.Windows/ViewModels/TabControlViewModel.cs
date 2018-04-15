using System;
using System.Collections.ObjectModel;
using Com.Ericmas001.Windows.Services.Interfaces;

namespace Com.Ericmas001.Windows.ViewModels
{
    public abstract class TabControlViewModel: BaseViewModel, ITabCreationViewModel
    {
        private readonly ITabFactoryService m_TabFactoryService;
        protected virtual NewTabViewModel CreateNewTab() 
        { 
            return null; 
        }
        protected virtual bool KeepNewTab => false;

        public ObservableCollection<BaseTabViewModel> Tabs { get; } = new ObservableCollection<BaseTabViewModel>();
        private NewTabViewModel m_NewTab;

        private BaseTabViewModel m_SelectedTab;

        protected TabControlViewModel(ITabFactoryService tabFactoryService)
        {
            m_TabFactoryService = tabFactoryService;
        }

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
                tab.OnCreateNewTab += (s, t, p) => AddTab(m_TabFactoryService.CreateTab(t, p));
                tab.OnRequestClose += OnTabClosed;
                tab.OnAttachDetachWindow += Tab_OnAttachDetachWindow;
                Tabs.Insert(Tabs.Count-1,tab);
                SelectedTab = tab;

                if (!KeepNewTab)
                    AddNewTab();
            }
        }

        private void Tab_OnAttachDetachWindow(object sender, BaseTabViewModel e)
        {
            var window = new MainTabWindow(new MainTabWindowViewModel(e, e.TabHeader, e.TabIcon, vm =>
            {
                AddTab(e);
                vm.CloseView();
            } ));
            window.Show();
            e.CloseView();
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
                {
                    m_NewTab.OnTabCreation += (s, t) => AddTab(t);
                    m_NewTab.OnCreateNewTab += (s, t, p) => AddTab(m_TabFactoryService.CreateTab(t, p));
                }

                Tabs.Add(m_NewTab);
            }
        }
        public void OnTabClosed(object sender, EventArgs e)
        {
            var tab = sender as BaseTabViewModel;
            if (tab == null)
                return;
            tab.OnRequestClose -= OnTabClosed;
            tab.OnAttachDetachWindow -= Tab_OnAttachDetachWindow;
            Tabs.Remove(tab);
        }
        public void SelectNewTab()
        {
            SelectedTab = m_NewTab;
        }
    }
}
