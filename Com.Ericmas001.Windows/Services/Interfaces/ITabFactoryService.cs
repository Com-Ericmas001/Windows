using System;
using Com.Ericmas001.Windows.ViewModels;
using Com.Ericmas001.Windows.ViewModels.Sections;

namespace Com.Ericmas001.Windows.Services.Interfaces
{
    public interface ITabFactoryService
    {
        NewTabViewModel CreateNewTab();
        TabSection CreateTabSection(Type t);
        BaseTabViewModel CreateTab(Type t, object parms);
    }
}
