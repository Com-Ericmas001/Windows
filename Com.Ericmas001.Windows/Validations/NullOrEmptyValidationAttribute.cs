using System;

namespace Com.Ericmas001.Windows.Validations
{
    public class NullOrEmptyValidationAttribute : SimpleValidationAttribute
    {
        public string Default { get; set; }
        public override string Validate(string value)
        {
            return string.IsNullOrEmpty(value) ? $"Ce paramètre est obligatoire!{(Default == null ? "" : $" (Défaut: {Default})")}" : null;
        }
    }
}
