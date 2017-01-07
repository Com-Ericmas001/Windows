// From https://denisdoucet.visualstudio.com/Apps/_versionControl?path=%24%2FApps%2FMain%2FCustomControls%2FControls%2FNumericUpDown.xaml.cs
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Com.Ericmas001.Windows.Xaml.CustomControls
{
    public partial class NumericUpDown : UserControl
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(decimal), typeof(NumericUpDown), new PropertyMetadata(0m));

        public decimal Value
        {
            get { return (decimal)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register("Minimum", typeof(decimal), typeof(NumericUpDown), new PropertyMetadata(0m));

        public decimal Minimum
        {
            get { return (decimal)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register("Maximum", typeof(decimal), typeof(NumericUpDown), new PropertyMetadata(100m));

        public decimal Maximum
        {
            get { return (decimal)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public static readonly DependencyProperty IncrementProperty = DependencyProperty.Register("Increment", typeof(decimal), typeof(NumericUpDown), new PropertyMetadata(1m));

        public decimal Increment
        {
            get { return (decimal)GetValue(IncrementProperty); }
            set { SetValue(IncrementProperty, value); }
        }

        public static readonly DependencyProperty DecimalPlacesProperty = DependencyProperty.Register("DecimalPlaces", typeof(int), typeof(NumericUpDown), new PropertyMetadata(0));

        public int DecimalPlaces
        {
            get { return (int)GetValue(DecimalPlacesProperty); }
            set { SetValue(DecimalPlacesProperty, value); }
        }

        public NumericUpDown()
        {
            InitializeComponent();
        }

        private void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[^0-9\-\.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            decimal val = Value;
            var str = textBox.Text.Trim();

            if (string.IsNullOrEmpty(str))
            {
                val = Minimum;
            }

            Regex regex = new Regex(@"^-?\d+\.?\d*$");
            if (regex.IsMatch(str))
            {
                val = Convert.ToDecimal(str);

                val = Math.Min(val, Maximum);
                val = Math.Max(val, Minimum);

                val = Math.Round(val / Increment, MidpointRounding.AwayFromZero) * Increment;
            }

            textBox.Text = val.ToString(string.Concat("F", DecimalPlaces), CultureInfo.InvariantCulture);
        }
    }
}