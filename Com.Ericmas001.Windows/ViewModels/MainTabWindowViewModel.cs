using System;
using System.Windows.Media;

namespace Com.Ericmas001.Windows.ViewModels
{
    public class MainTabWindowViewModel : BaseViewModel
    {
        private readonly Action<MainTabWindowViewModel> m_OnAttachFnct;
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

        public MainTabWindowViewModel(BaseTabViewModel content, string name, ImageSource icon, Action<MainTabWindowViewModel> onAttachFnct)
        {
            m_OnAttachFnct = onAttachFnct;
            Content = content;
            Name = name;
            Width = 1152;
            Height = 864;
            Icon = icon;
            content.OnAttachDetachWindow += ContentOnOnAttachDetachWindow;
        }

        private void ContentOnOnAttachDetachWindow(object sender, BaseTabViewModel baseTabViewModel)
        {
            m_OnAttachFnct(this);
            baseTabViewModel.OnAttachDetachWindow -= ContentOnOnAttachDetachWindow;
        }
    }
}
