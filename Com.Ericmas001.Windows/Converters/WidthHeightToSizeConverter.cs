using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Com.Ericmas001.Windows.Converters
{
    public class WidthHeightToSizeConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new Size((double)value[0], (double)value[1]);
        }

        // No need to implement converting back on a one-way binding 
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}