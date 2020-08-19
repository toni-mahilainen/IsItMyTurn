using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IsItMyTurn.Extensions
{
    [ContentProperty(nameof(Percent))]
    public class PercentOfWidthExtension : IMarkupExtension
    {
        public float Percent { get; set; }

        [System.ComponentModel.TypeConverter(typeof(TypeTypeConverter))]
        public Type TargetType { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            double displayWidth = (double)Application.Current.Properties["DisplayWidth"];
            return displayWidth * Percent;
        }
    }
}
