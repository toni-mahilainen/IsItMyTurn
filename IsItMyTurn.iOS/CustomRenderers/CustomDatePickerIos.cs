using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using IsItMyTurn.CustomRenderers;
using IsItMyTurn.iOS.CustomRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDatePickerIos))]
namespace IsItMyTurn.iOS.CustomRenderers
{
    public class CustomDatePickerIos : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                Control.Layer.CornerRadius = 15;
                Control.Layer.BackgroundColor = Color.Black.ToCGColor();
                Control.Layer.Opacity = 0.6f;
                Control.TextColor = Color.White.ToUIColor();
                Control.TextAlignment = UITextAlignment.Center;
            }
        }
    }
}