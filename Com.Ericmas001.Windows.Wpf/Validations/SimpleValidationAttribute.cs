﻿using System;

namespace Com.Ericmas001.Windows.Wpf.Validations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public abstract class SimpleValidationAttribute : Attribute
    {
        public abstract string Validate(string value);
    }
}