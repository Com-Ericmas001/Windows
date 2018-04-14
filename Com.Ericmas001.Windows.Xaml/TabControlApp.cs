using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Com.Ericmas001.Windows.Xaml.Windows;

namespace Com.Ericmas001.Windows.Xaml
{
    public class TabControlApp : Application
    {
        public TabControlApp(ITabControlAppParms parms)
        {
            foreach (var rd in parms.ResourceDictionaries)
            {
                Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri(rd, UriKind.Relative) });
            }
            foreach (var r in parms.Resources)
            {
                var ctor = r.Value.GetConstructor(Type.EmptyTypes);
                var obj = ctor?.Invoke(new object[0]);
                Resources.Add(r.Key, obj);
            }
            var menuDic = new ResourceDictionary();
            foreach (var cat in parms.Categories)
            {
                DataTemplate template = new DataTemplate
                {
                    DataType = cat.MenuViewModelType,
                    VisualTree = new FrameworkElementFactory(cat.MenuViewType)
                };
                menuDic.Add(new DataTemplateKey(cat.MenuViewModelType), template);
            }
            Resources.MergedDictionaries.Add(menuDic);
            var mainTabsDic = new ResourceDictionary();
            foreach (var mtv in parms.MainTabViews)
            {
                DataTemplate template = new DataTemplate
                {
                    DataType = mtv.Key,
                    VisualTree = new FrameworkElementFactory(mtv.Value)
                };
                mainTabsDic.Add(new DataTemplateKey(mtv.Key), template);
            }
            Resources.MergedDictionaries.Add(mainTabsDic);
            var imagesDic = new ResourceDictionary();
            foreach (var img in parms.Images)
            {
                imagesDic.Add(img.Key, new BitmapImage(new Uri("pack://application:,,/" + img.Value)));
            }
            Resources.MergedDictionaries.Add(imagesDic);

            MainWindow = new TabControlWindow(parms);
            MainWindow.Show();
        }
    }
}
