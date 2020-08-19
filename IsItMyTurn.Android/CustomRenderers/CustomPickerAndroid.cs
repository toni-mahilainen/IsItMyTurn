using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using IsItMyTurn.CustomRenderers;
using IsItMyTurn.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerAndroid))]
namespace IsItMyTurn.Droid.CustomRenderers
{
    public class CustomPickerAndroid : PickerRenderer
    {
        public CustomPickerAndroid(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                // Properties to custom picker
                GradientDrawable gradientDrawable = new GradientDrawable();
                gradientDrawable.SetCornerRadius(10f);
                gradientDrawable.SetColor(Android.Graphics.Color.Black);
                gradientDrawable.SetAlpha(153);
                Control.SetPadding(20, 10, 20, 10);
                Control.Gravity = GravityFlags.Center;
                Control.SetBackground(gradientDrawable);
            }
        }
    }
}