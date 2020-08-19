using IsItMyTurn.Models;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IsItMyTurn
{
    public partial class App : Application
    {
        public App(double displayHeight, double displayWidth)
        {
            // Save display height and width to properties
            Current.Properties["DisplayWidth"] = displayWidth;
            Current.Properties["DisplayHeight"] = displayHeight;
            Current.SavePropertiesAsync();
            
            InitializeComponent();
            var mainPage = new MainPage();

            MainPage = new NavigationPage(mainPage);
            NavigationPage.SetHasNavigationBar(mainPage, false);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
