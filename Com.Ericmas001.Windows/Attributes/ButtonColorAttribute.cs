﻿using System;

namespace Com.Ericmas001.Windows.Attributes
{
    public class ButtonColorAttribute : Attribute
    {
        public string Color { get; private set; }

        public ButtonColorAttribute(string color)
        {
            Color = color;
        }
    }
}
