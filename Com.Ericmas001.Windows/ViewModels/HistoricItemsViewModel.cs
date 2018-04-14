using System;
using System.Collections.Generic;

namespace Com.Ericmas001.Windows.ViewModels
{
    public class HistoricItemsViewModel : BaseViewModel
    {
        public event EmptyHandler OnTextChanged = delegate { };
        public int MaxHistory { get; set; }
        public FastObservableCollection<string> Items { get; }
        public IList<string> AllItems => Items;

        private string m_CurrentItem;

        public HistoricItemsViewModel(IList<string> initialItems = null)
        {
            MaxHistory = 25;
            Items = new FastObservableCollection<string>();
            if (initialItems != null)
                Items.AddItems(initialItems);
            CurrentItem = MostRecent;
        }

        public string MostRecent => Count > 0 ? Items[0] : "";

        public int Count => Math.Min(Items.Count, MaxHistory);

        public string CurrentItem
        {
            get => m_CurrentItem;
            set
            {
                Set(ref m_CurrentItem, value);
                OnTextChanged();
            }
        }

        public void AddCurrentItem()
        {
            if (Items.Contains(CurrentItem))
                Items.Move(Items.IndexOf(CurrentItem), 0);
            else
                Items.Insert(0, CurrentItem);
            CurrentItem = MostRecent;
        }
    }
}
