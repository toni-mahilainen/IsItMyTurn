﻿using IsItMyTurn.Models;
using IsItMyTurn.Pages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IsItMyTurn
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            //AppNameLbl.FontSize = Device.RuntimePlatform == Device.iOS ? 24 :
            //    Device.RuntimePlatform == Device.Android ? Device.GetNamedSize(NamedSize.Title, AppNameLbl) : Device.GetNamedSize(NamedSize.Title, AppNameLbl);
        }

        protected override void OnAppearing()
        {
            GetApartmentForCurrentShift();
        }

        private async void GetApartmentForCurrentShift()
        {
            // Apartment number for current shift to main page
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://isitmyturnapi.azurewebsites.net/api/apartment/currentshift");
            
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                LoadingLbl.IsVisible = false;
                LoadingLblFrame.IsVisible = false;
                CurrentShiftLbl.IsVisible = true;
                CurrentShiftLbl.Text = responseBody;
            }
        }

        private void AddNewBtn_Clicked(object sender, EventArgs e)
        {
            // To Add new shift -page
            var addNewPage = new AddNew();
            Navigation.PushAsync(addNewPage);
            NavigationPage.SetHasNavigationBar(addNewPage, false);
        }

        private void SeekAndDestroyBtn_Clicked(object sender, EventArgs e)
        {
            // To Seek and destroy -page
            var seekAndDestroyPage = new SeekAndDestroy();
            Navigation.PushAsync(seekAndDestroyPage);
            NavigationPage.SetHasNavigationBar(seekAndDestroyPage, false);
        }

        private void RefreshBtn_Clicked(object sender, EventArgs e)
        {
            // Refresh the main page 
            var vUpdatedPage = new MainPage();
            Navigation.InsertPageBefore(vUpdatedPage, this);
            NavigationPage.SetHasNavigationBar(vUpdatedPage, false);
            Navigation.PopAsync();
        }
    }
}
