using Com.Ericmas001.DataItems.Fields;

namespace Com.Ericmas001.Windows.Wpf.ViewModels.SearchElements
{
    [FieldType(FieldTypeEnum.UpperText)]
    public class UpperTextSearchElement : TextSearchElement
    {
        public override string TextValue
        {
            get { return Valeur.ToUpper(); }
        }
    }
}
