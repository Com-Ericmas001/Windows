using System;
using System.Windows.Controls;
using Com.Ericmas001.Windows.ViewModels;

namespace Com.Ericmas001.Windows
{
    /// <summary>
    /// Interaction logic for MainTabWindow.xaml
    /// </summary>
    public partial class MainTabWindow
    {
        public MainTabWindow(MainTabWindowViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
            if (Resources.Contains("ResultsTreeTemplateSelector"))
                Presenter.ContentTemplateSelector = Resources["ResultsTreeTemplateSelector"] as DataTemplateSelector;
            vm.OnRequestClose += (sender, args) => Close();
        }
    }
}
