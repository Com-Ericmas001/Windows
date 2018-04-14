using Com.Ericmas001.Common.Attributes;
using Com.Ericmas001.Windows.Demo.TabControlApp.Attributes;

namespace Com.Ericmas001.Windows.Demo.TabControlApp.Enums
{
    public enum AppCategoryEnum
    {

        [DisplayName("First Category")]
        [Color("DeepSkyBlue")]
        [Priority(10)]
        FirstCategory,

        [DisplayName("Hidden Category")]
        [Color("OrangeRed")]
        [Priority(20)]
        [Hidden]
        HiddenCategory,
    }
}
