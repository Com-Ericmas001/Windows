﻿using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shell;
using System.Xml;
using Com.Ericmas001.Windows.ViewModels;
using Com.Ericmas001.Windows.Xaml.Helpers;

namespace Com.Ericmas001.Windows.Xaml.Windows
{
    public class TabControlWindow : Window
    {
        public static TabControlWindow Instance { get; private set; }

        public TabControlWindow(ITabControlAppParms parms)
        {
            Instance = this;
            var context = parms.MainViewModel;
            context.AddNewTab();
            DataContext = context;
            Loaded += BaseMainWindow_Loaded;
            Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("/Com.Ericmas001.Windows.Xaml;component/Templates/TabTemplate.xaml", UriKind.Relative) });
            TaskbarItemInfo = new TaskbarItemInfo();
            SetBinding(TitleProperty, new Binding("Title"));
            Width = 800;
            Height = 600;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            WindowState = WindowState.Maximized;
            BindingOperations.SetBinding(TaskbarItemInfo, TaskbarItemInfo.ProgressStateProperty, new Binding("ProgressState"));
            BindingOperations.SetBinding(TaskbarItemInfo, TaskbarItemInfo.ProgressValueProperty, new Binding("ProgressValue"));
            Content = new Grid();
            Icon = string.IsNullOrEmpty(parms.MainIconName) ? null : Application.Current.FindResource(parms.MainIconName) as ImageSource;
        }
        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);

            TabControl tc = new TabControl
            {
                ItemTemplate = Application.Current.Resources["TabHeaderDataTemplate"] as DataTemplate
            };
            tc.SetBinding(ItemsControl.ItemsSourceProperty, new Binding("Tabs"));
            tc.SetBinding(Selector.SelectedItemProperty, new Binding("SelectedTab"));
            tc.SetValue(TabControlHelper.IsCachedProperty, true);
            tc.Loaded += (sender, args) => tc.SelectedIndex = 0;
            ((IAddChild)newContent).AddChild(tc);

        }
        void BaseMainWindow_Loaded(object s, RoutedEventArgs rea)
        {
            if (DataContext is IAppWindowViewModel mvm)
            {
                Activated += (sender, e) => mvm.IsWindowActive = true;
                Deactivated += (sender, e) => mvm.IsWindowActive = false;
            }
        }
    }
}
