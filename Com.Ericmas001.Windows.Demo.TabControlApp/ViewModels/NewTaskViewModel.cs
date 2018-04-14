using System.Collections.Generic;
using System.Linq;
using Com.Ericmas001.Common;
using Com.Ericmas001.Windows.Demo.TabControlApp.Attributes;
using Com.Ericmas001.Windows.Demo.TabControlApp.Enums;
using Com.Ericmas001.Windows.ViewModels;
using Com.Ericmas001.Windows.ViewModels.Sections;

namespace Com.Ericmas001.Windows.Demo.TabControlApp.ViewModels
{
    public class NewTaskViewModel : MultiCategoriesNewTabViewModel<AppCategoryEnum>
    {
        public NewTaskViewModel() => Sections.OfType<MyCategorySection>().First(x => x.IsExpanded).IsExpanded = false;

        protected override string IconBigImageName => "Demo";
        public override string TabTitle => "Demo TabControlApp";

        protected override TabSection CreateCategorySection(AppCategoryEnum cat) => MyCategorySection.CreateSection(cat);

        protected override IEnumerable<AppCategoryEnum> ExcludeCategories(IEnumerable<AppCategoryEnum> categories) => categories.Where(x => x.GetAttribute<HiddenAttribute>() == null);
    }
}
