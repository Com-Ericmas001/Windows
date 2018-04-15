using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Ericmas001.DependencyInjection;
using Com.Ericmas001.Windows.Services;
using Com.Ericmas001.Windows.Services.Interfaces;
using Com.Ericmas001.Windows.ViewModels;

namespace Com.Ericmas001.Windows
{
    public class TabControlWindowRegistrant : AbstractRegistrant
    {
        public TabControlWindowRegistrant(ITabControlAppParms parms)
        {
            RegisterInstance(parms);
        }
        protected override void RegisterEverything()
        {
            Register<ITabCreationViewModel, TabControlWindowViewModel>();
            Register<NewTabViewModel, MultiCategoriesNewTabViewModel>();
            Register<ITabFactoryService, TabFactoryService>();
        }
    }
}
