using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Ericmas001.Windows.ViewModels;

namespace Com.Ericmas001.Windows
{
    public interface ITabControlWindowParms
    {
        bool CacheNewTab { get; }
        string AppTitle { get; }
        string MainIconName { get; }
        NewTabViewModel CreateNewTab();
        ITabCreationViewModel MainViewModel { get; }
        IEnumerable<string> ResourceDictionaries { get; }
        IEnumerable<AppCategory> Categories { get; }
        Dictionary<Type, Type> MainTabViews { get; }
        Dictionary<string, string> Images { get; }
        int MenuSectionsWidth { get; }
    }
}
