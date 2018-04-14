using System;
using System.Collections.Generic;
using Com.Ericmas001.Windows.Demo.TabControlApp.ViewModels.FirstCategory;
using Com.Ericmas001.Windows.Demo.TabControlApp.Views.MainTabViews;
using Com.Ericmas001.Windows.Demo.TabControlApp.Views.MenuViews;
using Com.Ericmas001.Windows.ViewModels;

namespace Com.Ericmas001.Windows.Demo.TabControlApp
{
    public class AppConfig : ITabControlWindowParms
    {
        public IEnumerable<string> ResourceDictionaries => new string[0];

        public IEnumerable<AppCategory> Categories => new[]
        {
            new AppCategory
            {
                Title = "First Category",
                MenuColor = "DeepSkyBlue",
                MenuViewModelType = typeof(FirstCategoryMenuViewModel),
                MenuViewType = typeof(FirstCategoryMenuView),
            }
        };

        public Dictionary<Type, Type> MainTabViews => new Dictionary<Type, Type>
        {
            {typeof(FirstCategoryMainTabViewModel), typeof(FirstCategoryMainTabView) }
        };
        public Dictionary<string, string> Images => new Dictionary<string, string>
        {
            {"Demo", "Resources/demo.png" },
            {"First", "Resources/first.png" },
            {"Hidden", "Resources/hidden.png" }
        };

        public int MenuSectionsWidth => 600;

        public ITabCreationViewModel MainViewModel => new TabControlWindowViewModel(this);
        public string MainIconName => "Demo";
        public bool CacheNewTab => false;
        public string AppTitle => "Demo TabControlApp";
        public NewTabViewModel CreateNewTab()
        {
            return new MultiCategoriesNewTabViewModel(this);
        }
    }
}
