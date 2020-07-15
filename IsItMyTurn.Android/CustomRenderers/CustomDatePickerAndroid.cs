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
using IsItMyTurn.CustomRenderers;
using IsItMyTurn.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDatePickerAndroid))]
namespace IsItMyTurn.Droid.CustomRenderers
{
    public class CustomDatePickerAndroid : DatePickerRenderer
    {
        public CustomDatePickerAndroid(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
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