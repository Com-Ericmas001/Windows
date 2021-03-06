﻿using System;
using System.Collections.Generic;
using System.Linq;
using Com.Ericmas001.Common;
using Com.Ericmas001.Common.Attributes;
using Com.Ericmas001.Windows.ViewModels.Sections;

namespace Com.Ericmas001.Windows.ViewModels
{
    public abstract class MultiCategoriesNewTabViewModel<TCategory> : NewTabViewModel
        where TCategory : struct
    {

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

        protected virtual IEnumerable<TCategory> ExcludeCategories(IEnumerable<TCategory> categories)
        {
            return categories;
        }

        private void AddAllCategoriesSection()
        {
            m_Sections.AddRange(ExcludeCategories(EnumUtil.AllValues<TCategory>()).OrderBy(x => ((Enum)(object)x).GetAttribute<PriorityAttribute>().Priority).ThenBy(x => ((Enum)(object)x).DisplayName()).Select(x => CreateSectionWithHandlers(CreateCategorySection(x))));
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

        protected virtual TabSection CreateCategorySection(TCategory cat)
        {
            throw new NotImplementedException("You must override CreateCategorySection to use it !");
        }

        protected virtual void Init()
        {

        }

        public MultiCategoriesNewTabViewModel()
        {
            Init();

            AddAllSections();
            AddAllHeaderActions();
            AddAllFooterActions();

            if (Sections.Any())
                Sections.First().IsExpanded = true;

        }

        private ActionButtonSection CreateActionWithHandlers(ActionButtonSection action)
        {
            action.OnTabCreation += (sender, tab) => CreateNewTab(tab);
            return action;
        }

        private TabSection CreateSectionWithHandlers(TabSection section)
        {
            section.OnAfterExpanded += section_OnAfterExpanded;
            section.OnTabCreation += (sender, tab) => CreateNewTab(tab);
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
