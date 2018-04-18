using System.Collections.Generic;
using Com.Ericmas001.Windows.Attributes;

namespace Com.Ericmas001.Windows.Demo.TabControlApp.ViewModels.FirstCategory.Models
{
    public class FirstCategorySettings
    {
        [DefaultValues("Text1", "Text2")]
        public IList<string> SomeTextHistory { get; set; }

        public bool SomeBool { get; set; }
    }
}
