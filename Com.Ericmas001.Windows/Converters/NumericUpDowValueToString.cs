//From https://denisdoucet.visualstudio.com/Apps/_versionControl?path=%24%2FApps%2FMain%2FCustomControls%2FConverter%2FNumericUpDowValueToString.cs
using System;
using System.Globalization;
using System.Windows.Data;

namespace Com.Ericmas001.Windows.Converters
{
    public class NumericUpDowValueToString : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var value = values[0] as decimal?;
            var decimalPlaces = values[1] as int?;

            if (value != null && decimalPlaces != null)
            {
                return ((decimal)value).ToString(string.Concat("F", decimalPlaces), CultureInfo.InvariantCulture);
            }
            else
            {
                return "0";
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new object[] { System.Convert.ToDecimal((string)value) };
        }
    }
}