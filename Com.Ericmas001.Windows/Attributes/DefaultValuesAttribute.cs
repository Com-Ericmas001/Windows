using System;
using System.Collections.Generic;

namespace Com.Ericmas001.Windows.Attributes
{
    public class DefaultValuesAttribute : Attribute
    {
        public DefaultValuesAttribute(params string[] defaults)
        {
            Defaults = defaults;
        }

        public IEnumerable<string> Defaults { get; }
    }
}
