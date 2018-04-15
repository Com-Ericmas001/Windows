using System.Windows;
using Com.Ericmas001.Windows.Xaml.Windows;
using Unity;

namespace Com.Ericmas001.Windows.Demo.TabControlApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var container = UnitiyConfig.CreateContainer();
            Xaml.TabControlApp.Init(this, container.Resolve<ITabControlAppParms>(), container.Resolve<TabControlWindow>());
        }
    }
}