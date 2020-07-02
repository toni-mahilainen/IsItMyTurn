using IsItMyTurn.Models;
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
        }

        protected override void OnAppearing()
        {
            GetApartmentForCurrentShift();
        }

        private async void GetApartmentForCurrentShift()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://isitmyturnapi.azurewebsites.net/api/apartment/currentshift");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            CurrentShiftLbl.Text = responseBody;
        }

        private void AddNewBtn_Clicked(object sender, EventArgs e)
        {
            var addNewPage = new AddNew();
            Navigation.PushAsync(addNewPage);
            NavigationPage.SetHasNavigationBar(addNewPage, false);
        }

        private void SeekAndDestroyBtn_Clicked(object sender, EventArgs e)
        {
            var seekAndDestroyPage = new SeekAndDestroy();
            Navigation.PushAsync(seekAndDestroyPage);
            NavigationPage.SetHasNavigationBar(seekAndDestroyPage, false);
        }

        private void RefreshBtn_Clicked(object sender, EventArgs e)
        {
            var vUpdatedPage = new MainPage();
            Navigation.InsertPageBefore(vUpdatedPage, this);
            NavigationPage.SetHasNavigationBar(vUpdatedPage, false);
            Navigation.PopAsync();
        }
    }
}
