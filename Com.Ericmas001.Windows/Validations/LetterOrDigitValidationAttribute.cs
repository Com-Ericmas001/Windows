using System;
using System.Linq;

namespace Com.Ericmas001.Windows.Validations
{
    public class LetterOrDigitValidationAttribute : SimpleValidationAttribute
    {
        public override string Validate(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            return !value.All(char.IsLetterOrDigit) ? "Only letters or digits are valid!" : null;
        }
    }
}
