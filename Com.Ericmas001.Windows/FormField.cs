using System;
using System.ComponentModel;
using System.Linq;
using Com.Ericmas001.Common;
using Com.Ericmas001.Common.Attributes;
using Com.Ericmas001.Windows.Attributes;
using Com.Ericmas001.Windows.Enums;

namespace Com.Ericmas001.Windows
{
    public class SpecialDefaultValues
    {
        public const string DATE_AUJOURDHUI = "##DateTime.Now##";
    }

    public class FormField<TEnum> : FormField<TEnum, string>
        where TEnum : struct
    {
        public static new FormField<TEnum>[] AllFields
        {
            get
            {
                return EnumUtil.AllValues<TEnum>().Select(x => new FormField<TEnum>(x)).ToArray();
            }
        }

        public FormField(TEnum field)
            : base(field)
        {

        }
    }

    public class FormField<TEnum, TValue> : BaseViewModel
        where TEnum : struct
    {
        public static FormField<TEnum, TValue>[] AllFields
        {
            get
            {
                return EnumUtil.AllValues<TEnum>().Select(x => new FormField<TEnum, TValue>(x)).ToArray();
            }
        }

        private TValue m_Value = default(TValue);

        public TEnum Field { get; private set; }
        public string Description { get; private set; }
        public int Priorite { get; private set; }
        public TextLengthEnum Length { get; private set; }

        public TValue Value
        {
            get
            {
                return m_Value;
            }
            set
            {
                m_Value = value;
                RaisePropertyChanged("Value");
            }
        }

        public FormField(TEnum field)
        {
            if (!typeof(TEnum).IsEnum)
                throw new Exception("<TEnum> must be of Enum type (" + typeof(TEnum).Name + ")");

            Field = field;
            Description = ((Enum)(object)field).DisplayName();

            PriorityAttribute prioAtt = ((Enum)(object)field).GetAttribute<PriorityAttribute>();
            if (prioAtt != null)
                Priorite = prioAtt.Priority;
            else
                Priorite = 9999;

            TextLengthAttribute lengthAtt = ((Enum)(object)field).GetAttribute<TextLengthAttribute>();
            if (lengthAtt != null)
                Length = lengthAtt.Length;
            else
                Length = TextLengthEnum.Normal;


            DefaultValueAttribute defAtt = ((Enum)(object)field).GetAttribute<DefaultValueAttribute>();
            if (defAtt != null && typeof(TValue) == typeof(string))
                Value = (TValue)Convert.ChangeType(GenerateDefaultStringValues(defAtt.Value.ToString()), typeof(string));
        }

        private string GenerateDefaultStringValues(string def)
        {
            switch (def)
            {
                case SpecialDefaultValues.DATE_AUJOURDHUI:
                    return DateTime.Now.ToString();
            }
            return def;      
        }
    }
}
