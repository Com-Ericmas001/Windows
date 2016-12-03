using System;
using System.Linq;

namespace Com.Ericmas001.Windows.Validations
{
    public class DigitValidationAttribute : SimpleValidationAttribute
    {
        public override string Validate(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            return !value.All(char.IsDigit) ? "Only digits are valid!" : null;
        }
    }
}
