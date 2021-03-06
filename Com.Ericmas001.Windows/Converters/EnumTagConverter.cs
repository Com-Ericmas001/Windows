﻿using System;
using System.Windows.Data;

namespace Com.Ericmas001.Windows.Converters
{
    public class EnumTagConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || String.IsNullOrEmpty(value.ToString()))
                return null;

            if (Com.Ericmas001.Common.EnumUtil.GetAttribute<Com.Ericmas001.Common.Attributes.TagAttribute>((Enum)value) != null)
                return Com.Ericmas001.Common.EnumUtil.Tag((Enum)value);

            return value.ToString();
        }


        // No need to implement converting back on a one-way binding 
        public object ConvertBack(object value, Type targetType,
          object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}