using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Com.Ericmas001.Windows.ViewModels.Trees
{
    public abstract class TreeElementViewModel : BaseViewModel
    {
        public event NewTabEventHandler OnTabCreation = delegate { };
        private bool m_IsExpanded = false;

        public virtual FontWeight FontWeight => FontWeights.Normal;
        public virtual FontFamily FontFamily => new FontFamily("Microsoft Sans Serif");
        public virtual FontStyle FontStyle => FontStyles.Normal;
        public virtual TextDecorationCollection FontDecoration => new TextDecorationCollection();
        public virtual Brush FontColor => Brushes.Black;

        public string Path => Parent == null ? null : (Parent.Path == null ? Parent.NameForPath : Parent.Path + " / " + Parent.NameForPath);

        public string PathAndName => Path == null ? NameForPath : Path + " / " + NameForPath;

        public virtual string NameForPath => Text;

        public bool IsExpanded
        {
            get { return m_IsExpanded; }
            set { Set(ref m_IsExpanded, value); }
        }
        private FastObservableCollection<TreeElementViewModel> m_Children;
        public FastObservableCollection<TreeElementViewModel> Children
        {
            get
            {
                if (m_Children == null)
                {
                    m_Children = new FastObservableCollection<TreeElementViewModel>();
                }
                return m_Children;
            }
        }
        public TreeElementViewModel Parent { get; private set; }
        public abstract string Text { get; }

        private FastObservableCollection<TreeContextMenuItemViewModel> m_ContextMenuItems;
        public FastObservableCollection<TreeContextMenuItemViewModel> ContextMenuItems
        {
            get
            {
                if (m_ContextMenuItems == null)
                {
                    m_ContextMenuItems = new FastObservableCollection<TreeContextMenuItemViewModel>();
                    PopulateContextMenu();
                }
                return m_ContextMenuItems;
            }
        }

        public TreeElementViewModel(TreeElementViewModel parent)
        {
            Parent = parent;
        }

        public bool HasOnlyOneLeaf => NbLeaves == 1;

        public TreeElementViewModel FirstLeaf => TreeLeaves.FirstOrDefault();

        public bool HasChildren => Children != null && Children.Any();

        public int NbChildren => Children != null ? Children.Count : 0;

        public int NbLeaves => HasChildren ? TreeLeaves.Count() : 0;

        public bool HasGrandChildren
        {
            get
            {
                return HasChildren && Children.Any(x => x.HasChildren);
            }
        }

        public bool CanExpand => HasChildren && !IsExpanded;

        public bool CanCollapse => HasChildren && IsExpanded;

        public void Expand()
        {
            IsExpanded = true;
        }

        public void ExpandAll()
        {
            Children.ToList().ForEach(x => x.ExpandAll());
            Expand();
        }

        public void ExpandWithParents()
        {
            Expand();
            if (Parent != null)
                Parent.ExpandWithParents();
        }

        public void Collapse()
        {
            IsExpanded = false;
        }

        public void CollapseAll()
        {
            Collapse();
            Children.ToList().ForEach(x => x.CollapseAll());
        }

        public virtual IEnumerable<TreeElementViewModel> TreeLeaves
        {
            get { return HasChildren ? Children.SelectMany(x => x.TreeLeaves) : new[] { this }; }
        }

        protected virtual void PopulateContextMenu()
        {
            ContextMenuItems.Add(new ExpandMenuItem(this));
            ContextMenuItems.Add(new CollapseMenuItem(this));
            ContextMenuItems.Add(new ExpandAllMenuItem(this));
            ContextMenuItems.Add(new CollapseAllMenuItem(this));
        }

        public void CreateTab(BaseTabViewModel tab)
        {
            OnTabCreation(this, tab);
        }

        public virtual void AddChildren(params TreeElementViewModel[] elems)
        {
            if (elems.Any())
                Children.AddItems(elems);
        }
    }
}
