using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Com.Ericmas001.Windows.Attributes;
using Com.Ericmas001.Windows.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Com.Ericmas001.Windows.Demo.TabControlApp.ViewModels.FirstCategory.Models
{
    public class FirstCategorySettings
    {
        [DefaultValues("Text1", "Text2")]
        public IList<string> SomeTextHistory { get; set; }

        public bool SomeBool { get; set; }
    }
}
