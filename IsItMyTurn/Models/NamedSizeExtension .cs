using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IsItMyTurn.Models
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

    [ContentProperty(nameof(Percent))]
    public class PercentOfHeightExtension : IMarkupExtension
    {
        public float Percent { get; set; }

        [System.ComponentModel.TypeConverter(typeof(TypeTypeConverter))]
        public Type TargetType { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return DeviceDisplay.MainDisplayInfo.Height * Percent;
        }
    }

    [ContentProperty(nameof(Percent))]
    public class PercentOfWidthExtension : IMarkupExtension
    {
        public float Percent { get; set; }

        [System.ComponentModel.TypeConverter(typeof(TypeTypeConverter))]
        public Type TargetType { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            System.Diagnostics.Debug.WriteLine($"######Width######  :  {DeviceDisplay.MainDisplayInfo.Density}");
            return DeviceDisplay.MainDisplayInfo.Width * Percent;
        }
    }
}
