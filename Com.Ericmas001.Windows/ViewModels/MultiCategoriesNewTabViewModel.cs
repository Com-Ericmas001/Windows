using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using Com.Ericmas001.Windows.Services.Interfaces;
using Com.Ericmas001.Windows.ViewModels.Sections;

namespace Com.Ericmas001.Windows.ViewModels
{
    public class MultiCategoriesNewTabViewModel : NewTabViewModel
    {
        private readonly ITabControlAppParms m_Parms;
        private readonly ITabFactoryService m_TabFactoryService;

        public MultiCategoriesNewTabViewModel(ITabControlAppParms parms, ITabFactoryService tabFactoryService)
        {
            m_Parms = parms;
            m_TabFactoryService = tabFactoryService;
            Init();

            AddAllSections();
            AddAllHeaderActions();
            AddAllFooterActions();

            if (Sections.Any())
                Sections.First().IsExpanded = true;

        }

        public override BaseTabViewModel CreateContentTab()
        {
            return null;
        }

        private readonly List<TabSection> m_Sections = new List<TabSection>();
        private readonly List<ActionButtonSection> m_HeaderActions = new List<ActionButtonSection>();
        private readonly List<ActionButtonSection> m_FooterActions = new List<ActionButtonSection>();

        public TabSection[] Sections => m_Sections.ToArray();
        public ActionButtonSection[] HeaderActions => m_HeaderActions.ToArray();

        public ActionButtonSection[] FooterActions => m_FooterActions.ToArray();
        public bool HasHeaderActions => m_HeaderActions.Any();

        public bool HasFooterActions => m_FooterActions.Any();

        protected virtual IEnumerable<AppCategory> ExcludeCategories(IEnumerable<AppCategory> categories)
        {
            return categories.Where(x => !x.Hidden);
        }

        private void AddAllCategoriesSection()
        {
            m_Sections.AddRange(ExcludeCategories(m_Parms.Categories).Select(x => CreateSectionWithHandlers(CreateCategorySection(x))));
        }

        protected virtual void AddAllSections()
        {
            AddAllCategoriesSection();
        }

        protected virtual void AddAllHeaderActions()
        {
        }

        protected virtual void AddAllFooterActions()
        {
        }

        protected void AddSection(TabSection section)
        {
            m_Sections.Add(CreateSectionWithHandlers(section));
        }

        protected void AddHeaderAction(ActionButtonSection action)
        {
            m_HeaderActions.Add(CreateActionWithHandlers(action));
        }

        protected void AddFooterAction(ActionButtonSection action)
        {
            m_FooterActions.Add(CreateActionWithHandlers(action));
        }

        protected virtual TabSection CreateCategorySection(AppCategory cat)
        {
            var section = m_TabFactoryService.CreateTabSection(cat.MenuViewModelType);
            if (section != null)
            {
                if (!string.IsNullOrEmpty(cat.ImageNameSmall))
                    section.Info.IconImageSmallName = cat.ImageNameSmall;

                if (!string.IsNullOrEmpty(cat.ImageNameBig))
                    section.Info.IconImageBigName = cat.ImageNameBig;

                if (!string.IsNullOrEmpty(cat.MenuColor))
                {
                    try
                    {
                        var bc = new BrushConverter();
                        section.Info.Background = (SolidColorBrush)bc.ConvertFromString(cat.MenuColor);
                    }
                    catch
                    {
                        //too bad
                    }
                }

                if (!string.IsNullOrEmpty(cat.MenuButtonColor))
                {
                    try
                    {
                        var bc = new BrushConverter();
                        section.Info.ButtonBrush = ((SolidColorBrush) bc.ConvertFromString(cat.MenuButtonColor)).Color;
                    }
                    catch
                    {
                        //too bad
                    }
                }

                section.Info.Description = cat.Title;
                section.Info.SectionWidth = m_Parms.MenuSectionsWidth;
            }

            return section;
        }

        protected void Init()
        {

        }
        protected override string IconBigImageName => m_Parms.MainIconName;
        public override string TabTitle => m_Parms.AppTitle;

        private ActionButtonSection CreateActionWithHandlers(ActionButtonSection action)
        {
            action.OnTabCreation += (sender, tab) => CreateNewTab(tab);
            action.OnCreateNewTab += (sender, vmType, parms) => CreateNewTab(vmType, parms);
            return action;
        }

        private TabSection CreateSectionWithHandlers(TabSection section)
        {
            section.OnAfterExpanded += section_OnAfterExpanded;
            section.OnTabCreation += (sender, tab) => CreateNewTab(tab);
            section.OnCreateNewTab += (sender, vmType, parms) => CreateNewTab(vmType, parms);
            return section;
        }

        void section_OnAfterExpanded(object sender, EventArgs e)
        {
            foreach (TabSection section in Sections)
                if (section != sender)
                    section.IsExpanded = false;
        }
    }
}
