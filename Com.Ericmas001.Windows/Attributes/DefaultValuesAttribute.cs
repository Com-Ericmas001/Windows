using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
