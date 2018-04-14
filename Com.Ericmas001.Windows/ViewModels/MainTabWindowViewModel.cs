using System;
using System.Windows;
using System.Windows.Media;

namespace Com.Ericmas001.Windows.ViewModels
{
    public class MainTabWindowViewModel : BaseViewModel
    {
        private double m_Width;
        private double m_Height;
        private ImageSource m_Icon;
        public BaseViewModel Content { get; }
        public string Name { get; }

        public double Width
        {
            get => m_Width;
            set => Set(ref m_Width, value);
        }
        public double Height
        {
            get => m_Height;
            set => Set(ref m_Height, value);
        }
        public ImageSource Icon
        {
            get => m_Icon;
            set => Set(ref m_Icon, value);
        }

        public MainTabWindowViewModel(BaseViewModel content, string name, Size s, string iconName)
        {
            Content = content;
            Name = name;
            Width = Math.Min(1152, s.Width);
            Height = Math.Min(864, s.Height);
            Icon = String.IsNullOrEmpty(iconName) ? null : Application.Current.FindResource(iconName) as ImageSource;
        }
    }
}
