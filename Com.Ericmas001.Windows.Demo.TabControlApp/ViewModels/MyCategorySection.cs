using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Com.Ericmas001.Common;
using Com.Ericmas001.Windows.Demo.TabControlApp.Attributes;
using Com.Ericmas001.Windows.Demo.TabControlApp.Enums;
using Com.Ericmas001.Windows.ViewModels;
using Com.Ericmas001.Windows.ViewModels.Sections;

namespace Com.Ericmas001.Windows.Demo.TabControlApp.ViewModels
{
    public class MyCategorySection : CategorySection<AppCategoryEnum>
    {
        public MyCategorySection(AppCategoryEnum cat)
            : base(cat)
        {
        }

        public override BaseTabViewModel CreateContentTab() => null;
        public override int SectionWidth => 600;


        private static Dictionary<AppCategoryEnum, Type> m_CategoryToSection;
        public static MyCategorySection CreateSection(AppCategoryEnum cat)
        {
            if (m_CategoryToSection == null)
            {
                m_CategoryToSection = new Dictionary<AppCategoryEnum, Type>();
                foreach (var type in Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(MyCategorySection))))
                    m_CategoryToSection.Add(type.GetAttributeValue<AppCategoryAttribute, AppCategoryEnum>(att => att.Category), type);
            }

            if (!m_CategoryToSection.ContainsKey(cat))
                return new MyCategorySection(cat);

            var ctor = m_CategoryToSection[cat].GetConstructor(new Type[0]);
            return ctor?.Invoke(new object[0]) as MyCategorySection;

        }
    }
}
