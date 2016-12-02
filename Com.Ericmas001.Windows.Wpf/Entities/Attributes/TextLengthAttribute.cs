using System;
using Com.Ericmas001.Windows.Wpf.Entities.Enums;

namespace Com.Ericmas001.Windows.Wpf.Entities.Attributes
{
    public class TextLengthAttribute : Attribute
    {
        public TextLengthEnum Length { get; private set; }

        public TextLengthAttribute(TextLengthEnum length)
        {
            Length = length;
        }
    }
}
