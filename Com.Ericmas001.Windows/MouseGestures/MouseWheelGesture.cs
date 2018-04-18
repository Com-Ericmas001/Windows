using System.Windows.Input;
using Com.Ericmas001.Windows.Enums;

namespace Com.Ericmas001.Windows.MouseGestures
{
    public class MouseWheelGesture : MouseGesture
    {
        public MouseWheelDirectionEnum Direction { get; set; }

        public MouseWheelGesture(ModifierKeys keys, MouseWheelDirectionEnum direction)
            : base(MouseAction.WheelClick, keys)
        {
            Direction = direction;
        }

        public override bool Matches(object targetElement, InputEventArgs inputEventArgs)
        {
            var args = inputEventArgs as MouseWheelEventArgs;

            if (args == null)
                return false;

            if (!base.Matches(targetElement, inputEventArgs))
                return false;

            if (Direction == MouseWheelDirectionEnum.Up && args.Delta > 0 || Direction == MouseWheelDirectionEnum.Down && args.Delta < 0)
            {
                inputEventArgs.Handled = true;
                return true;
            }

            return false;
        }
    }
}
