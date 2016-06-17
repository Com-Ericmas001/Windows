using System;
using System.Linq;

namespace Com.Ericmas001.Windows.Attributes.Validations
{
    public class LetterOrDigitValidationAttribute : SimpleValidationAttribute
    {
        public override string Validate(string value)
        {
            if (String.IsNullOrEmpty(value))
                return null;
            return !value.All(Char.IsLetterOrDigit) ? String.Format("Seul des lettres ou des chiffres sont acceptés!") : null;
        }
    }
}
