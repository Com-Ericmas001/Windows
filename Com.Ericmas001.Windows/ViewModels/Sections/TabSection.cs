using System;

namespace Com.Ericmas001.Windows.ViewModels.Sections
{
    public abstract class TabSection : NewTabViewModel
    {
        public event EventHandler OnAfterExpanded = delegate { };

        private bool m_IsExpanded;

        public bool IsExpanded
        {
            get { return m_IsExpanded; }
            set
            {
                if (m_IsExpanded != value)
                {
                    Set(ref m_IsExpanded, value);
                    if (m_IsExpanded)
                        OnAfterExpanded(this, new EventArgs());
                }
            }
        }

        public TabSectionInfo Info { get; protected set; } = new TabSectionInfo();

        public virtual int SectionWidth => Info.SectionWidth;
        public override BaseTabViewModel CreateContentTab()
        {
            return null;
        }
    }
}
