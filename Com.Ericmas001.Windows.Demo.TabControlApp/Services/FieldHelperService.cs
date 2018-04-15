using System;
using Com.Ericmas001.Windows.Demo.TabControlApp.Services.Interfaces;

namespace Com.Ericmas001.Windows.Demo.TabControlApp.Services
{
    public class FieldHelperService : IFieldHelperService
    {
        public string SomeTextHelp() => "(Just write something ...)";
        public string SomeBoolHelp() => "(Just check it ... or not !)";
    }
}
