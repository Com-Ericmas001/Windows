﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
