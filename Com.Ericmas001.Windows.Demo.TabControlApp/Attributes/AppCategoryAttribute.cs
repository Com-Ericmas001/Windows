using System;
using Com.Ericmas001.Windows.Demo.TabControlApp.Enums;

namespace Com.Ericmas001.Windows.Demo.TabControlApp.Attributes
{
    public class AppCategoryAttribute : Attribute
    {
        public AppCategoryEnum Category { get; }
        public AppCategoryAttribute(AppCategoryEnum cat)
        {
            Category = cat;
        }
    }
}
