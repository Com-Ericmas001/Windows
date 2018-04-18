using Com.Ericmas001.DependencyInjection.Unity;
using Com.Ericmas001.Windows.Demo.TabControlApp.Services;
using Com.Ericmas001.Windows.Demo.TabControlApp.Services.Interfaces;
using Unity;

namespace Com.Ericmas001.Windows.Demo.TabControlApp
{
    public class UnitiyConfig
    {
        public static IUnityContainer CreateContainer()
        {
            IUnityContainer container = new UnityContainer();

            new TabControlWindowRegistrant(new MyApp()).RegisterTypes(container);

            container.RegisterType<IFieldHelperService, FieldHelperService>();

            return container;
        }
    }
}
