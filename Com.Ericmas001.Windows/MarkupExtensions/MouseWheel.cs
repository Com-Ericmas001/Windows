using System;
using System.Windows.Input;
using System.Windows.Markup;
using Com.Ericmas001.Windows.Enums;
using Com.Ericmas001.Windows.MouseGestures;

namespace Com.Ericmas001.Windows.MarkupExtensions
{
    public class MouseWheel : MarkupExtension
    {
        public MouseWheelDirectionEnum Direction { get; set; }
        public ModifierKeys Keys { get; set; }

        public MouseWheel()
        {
            Keys = ModifierKeys.None;
            Direction = MouseWheelDirectionEnum.Down;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new MouseWheelGesture(Keys, Direction);
        }
    }
}
