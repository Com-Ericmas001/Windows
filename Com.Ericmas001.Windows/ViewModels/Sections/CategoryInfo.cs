using System;
using System.Windows.Media;
using Com.Ericmas001.Common;
using Com.Ericmas001.Common.Attributes;
using Com.Ericmas001.Windows.Attributes;

namespace Com.Ericmas001.Windows.ViewModels.Sections
{
    public class CategoryInfo<TCategory> : TabSectionInfo
        where TCategory : struct
    {

        public CategoryInfo(TCategory cat)
        {
            var imgAtt = ((Enum)(object)cat).GetAttribute<ImageSourceAttribute>();
            if (imgAtt != null)
            {
                IconImageSmallName = imgAtt.ImageNameSmall;
                IconImageBigName = imgAtt.ImageNameBig;
            }

            var brushAtt = ((Enum)(object)cat).GetAttribute<ColorAttribute>();
            if (brushAtt != null)
            {
                try
                {
                    var bc = new BrushConverter();
                    Background = (SolidColorBrush)bc.ConvertFromString(brushAtt.Color);
                }
                catch { }
            }

            var bbrushAtt = ((Enum)(object)cat).GetAttribute<ButtonColorAttribute>();
            if (bbrushAtt != null)
            {
                try
                {
                    var bc = new BrushConverter();
                    ButtonBrush = ((SolidColorBrush)bc.ConvertFromString(bbrushAtt.Color)).Color;
                }
                catch { }
            }

            Description = ((Enum)(object)cat).DisplayName();

            var prioAtt = ((Enum)(object)cat).GetAttribute<PriorityAttribute>();
            if (prioAtt != null)
                Priorite = prioAtt.Priority;
        }

    }
}
