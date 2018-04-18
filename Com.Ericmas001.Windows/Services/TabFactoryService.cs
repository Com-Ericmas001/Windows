using System;
using System.Linq;
using Com.Ericmas001.DependencyInjection.Resolvers.Interfaces;
using Com.Ericmas001.Windows.Services.Interfaces;
using Com.Ericmas001.Windows.ViewModels;
using Com.Ericmas001.Windows.ViewModels.Sections;

namespace Com.Ericmas001.Windows.Services
{
    public class TabFactoryService : ITabFactoryService
    {
        private readonly IResolverService m_Resolver;

        public TabFactoryService(IResolverService resolver)
        {
            m_Resolver = resolver;
        }
        public NewTabViewModel CreateNewTab()
        {
            return m_Resolver.Resolve<NewTabViewModel>();
        }

        public TabSection CreateTabSection(Type t)
        {
            return m_Resolver.Resolve(t) as TabSection;
        }

        public BaseTabViewModel CreateTab(Type t, object parms)
        {
            var tab = m_Resolver.Resolve(t) as BaseTabViewModel;

            var parmProperty = t.GetProperties().Single(p => p.Name == "Parms" && p.PropertyType == parms.GetType());
            parmProperty.SetValue(tab,parms);

            tab.OnLoadFinished();

            return tab;
        }
    }
}
