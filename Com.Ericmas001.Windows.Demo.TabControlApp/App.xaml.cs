using System.Windows;
using Com.Ericmas001.Windows.Xaml.Windows;

namespace Com.Ericmas001.Windows.Demo.TabControlApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            Xaml.TabControlApp.Init(this, new MyApp());
        }
    }
}