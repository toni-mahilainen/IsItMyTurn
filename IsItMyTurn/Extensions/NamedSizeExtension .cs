using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IsItMyTurn.Extensions
{
    [ContentProperty(nameof(NamedSize))]
    public class NamedSizeExtension : IMarkupExtension
    {
        public NamedSize NamedSize { get; set; }

        [System.ComponentModel.TypeConverter(typeof(TypeTypeConverter))]
        public Type TargetType { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return Device.GetNamedSize(NamedSize, TargetType);
        }
    }
}
