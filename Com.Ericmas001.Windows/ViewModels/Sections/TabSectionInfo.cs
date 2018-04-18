using System.Windows;
using System.Windows.Media;
using Com.Ericmas001.Windows.Util;

namespace Com.Ericmas001.Windows.ViewModels.Sections
{
    public class TabSectionInfo
    {
        public TabSectionInfo()
        {
            IconImageSmallName = string.Empty;
            IconImageBigName = string.Empty;
            Background = Brushes.Gray;
            ButtonBrush = Colors.DarkSlateGray;
            Description = string.Empty;
            Priorite = 100;
            SectionWidth = 400;
        }

        public int SectionWidth { get; set; }
        public string IconImageSmallName { get; set; }

        public string IconImageBigName { get; set; }

        public SolidColorBrush Background { get; set; }

        public Color ButtonBrush { get; set; }

        public string Description { get; set; }

        public int Priorite { get; set; }

        public SolidColorBrush HeaderForeground => ColorUtil.GetForegroundFromBackground(Background);

        public virtual ImageSource IconImageSmall => string.IsNullOrEmpty(IconImageSmallName) ? null : Application.Current.FindResource(IconImageSmallName) as ImageSource;
        public virtual ImageSource IconImageBig => string.IsNullOrEmpty(IconImageBigName) ? null : Application.Current.FindResource(IconImageBigName) as ImageSource;
    }
}
