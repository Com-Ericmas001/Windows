using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Controls;
using Com.Ericmas001.Windows.Converters;
using Com.Ericmas001.Windows.Demo.TabControlApp.ViewModels.FirstCategory;
using Com.Ericmas001.Windows.Demo.TabControlApp.Views.MainTabViews;
using Com.Ericmas001.Windows.Demo.TabControlApp.Views.MenuViews;
using Com.Ericmas001.Windows.ViewModels;

namespace Com.Ericmas001.Windows.Demo.TabControlApp
{
    public class MyApp : ITabControlAppParms
    {
        [STAThread]
        public static void Main() => new Xaml.TabControlApp(new MyApp()).Run();


        public string AppTitle => "Demo TabControlApp";
        public string MainIconName => "Demo";
        public Dictionary<string, string> Images => new Dictionary<string, string>
        {
            {"Demo", "Resources/demo.png" },
            {"First", "Resources/first.png" },
            {"Hidden", "Resources/hidden.png" }
        };

        public IEnumerable<string> ResourceDictionaries => new[]
        {
            "/Com.Ericmas001.Windows.Xaml;component/Styles/ValidationStyles.xaml",
            "/Com.Ericmas001.Windows.Xaml;component/Styles/ExpanderStyles.xaml",
            "/Com.Ericmas001.Windows.Xaml;component/Styles/ButtonStyles.xaml",
            "/Com.Ericmas001.Windows.Xaml;component/Styles/RadioButtonStyles.xaml",
            "/Com.Ericmas001.Windows.Xaml;component/Styles/TreeItemStyles.xaml",
            "/Com.Ericmas001.Windows.Xaml;component/Styles/TextboxStyles.xaml",
            "/Com.Ericmas001.Windows.Xaml;component/Resources/ImageResources.xaml",
            "/Com.Ericmas001.Windows.Xaml;component/Templates/TabTemplate.xaml",
            "/Com.Ericmas001.Windows.Xaml;component/Templates/LoadingDataTemplate.xaml"
        };
        public Dictionary<string,Type> Resources => new Dictionary<string, Type>
        {
            {"EnumConverter", typeof(EnumMatchToBooleanConverter)},
            {"EnumDescConverter", typeof(EnumDisplayNameConverter)},
            {"EnumTagConverter", typeof(EnumTagConverter)},
            {"EnumAbbreviationConverter", typeof(EnumAbbreviationConverter)},
            {"EnumDisplayNameConverter", typeof(EnumDisplayNameConverter)},
            {"SizeConverter", typeof(WidthHeightToSizeConverter)},
            {"EnumFlagConverter", typeof(EnumFlagConverter)},
            {"BoolToVisConverter", typeof(BooleanToVisibilityConverter)},
        };

        public ITabCreationViewModel MainViewModel => new TabControlWindowViewModel(this);
        public NewTabViewModel CreateNewTab() => new MultiCategoriesNewTabViewModel(this);

        public int MenuSectionsWidth => 600;
        public bool CacheNewTab => false;

        public IEnumerable<AppCategory> Categories => new []
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
    }
}
