using System.Windows.Controls;
using Com.Ericmas001.Windows.ViewModels;

namespace Com.Ericmas001.Windows.Xaml.Windows
{
    /// <summary>
    /// Interaction logic for MainTabWindow.xaml
    /// </summary>
    public partial class MainTabWindow
    {
        public MainTabWindow(MainTabWindowViewModel vm)
        {
            InitializeComponent();
            foreach (var resource in BaseMainWindow.Instance.Resources.Keys)
            {
                Resources.Add(resource, BaseMainWindow.Instance.Resources[resource]);
            }
            foreach (var resourceDictionary in BaseMainWindow.Instance.Resources.MergedDictionaries)
            {
                Resources.MergedDictionaries.Add(resourceDictionary);
            }
            DataContext = vm;
            if (Resources.Contains("ResultsTreeTemplateSelector"))
                Presenter.ContentTemplateSelector = Resources["ResultsTreeTemplateSelector"] as DataTemplateSelector;
        }
    }
}
