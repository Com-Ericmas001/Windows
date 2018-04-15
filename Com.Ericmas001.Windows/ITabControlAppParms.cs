using System;
using System.Collections.Generic;
using Com.Ericmas001.Windows.ViewModels;

namespace Com.Ericmas001.Windows
{
    public interface ITabControlAppParms
    {
        string AppTitle { get; }
        string MainIconName { get; }

        Dictionary<string, string> Images { get; }
        IEnumerable<string> ResourceDictionaries { get; }
        Dictionary<string, Type> Resources { get; }

        int MenuSectionsWidth { get; }
        bool CacheNewTab { get; }
        IEnumerable<AppCategory> Categories { get; }
        Dictionary<Type, Type> MainTabViews { get; }
    }
}
