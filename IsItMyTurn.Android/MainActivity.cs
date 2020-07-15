using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Common;
using Firebase.Messaging;
using Firebase.Iid;
using Android.Util;
using Firebase;
using Android.Gms.Tasks;
using Java.Lang;

namespace IsItMyTurn.Droid
{
    [Activity(Label = "IsItMyTurn", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {   
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Xamarin.Forms.Forms.Init(this, savedInstanceState);
            // Density i.e., pixels per inch or cms  
            var scale = Resources.DisplayMetrics.Density;
            // Display width in pixels  
            var widthInPixels = Resources.DisplayMetrics.WidthPixels;
            //Display height in pixels  
            var heightInPixels = Resources.DisplayMetrics.HeightPixels;
            // Display width in units  
            double widthInUnits = ((widthInPixels - 0.5f) / scale);
            // Display height in units  
            double heightInUnits = ((heightInPixels - 0.5f) / scale);

            // Initialize app for Firebase
            FirebaseApp.InitializeApp(this);
            LoadApplication(new App(heightInUnits, widthInUnits));
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}