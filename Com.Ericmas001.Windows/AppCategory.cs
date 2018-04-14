using System;

namespace Com.Ericmas001.Windows
{
    public class AppCategory
    {
        public string Title { get; set; }
        public string MenuColor { get; set; }
        public string MenuButtonColor { get; set; }
        public Type MenuViewModelType { get; set; }
        public Type MenuViewType { get; set; }
        public string ImageNameSmall { get; set; }
        public string ImageNameBig { get; set; }
        public bool Hidden { get; set; }
    }
}
