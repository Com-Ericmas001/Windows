using System;

namespace Com.Ericmas001.Windows.Validations
{
    public class MinDigitValidationAttribute : DigitValidationAttribute
    {
        private readonly int m_Min;

        public MinDigitValidationAttribute(int min)
        {
            m_Min = min;
        }
        public override string Validate(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            if (string.IsNullOrEmpty(base.Validate(value)))
                return null;

            return int.Parse(value) < m_Min ? $"The number must be >= {m_Min}!" : null;
        }
    }
}
