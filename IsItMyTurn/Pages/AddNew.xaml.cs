using IsItMyTurn.Models;
using IsItMyTurn.Pages;
using IsItMyTurn.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IsItMyTurn
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNew : ContentPage
    {
        public AddNew()
        {
            InitializeComponent();
            BindingContext = new PickerViewModel();
        }

        protected override void OnAppearing()
        {
            // Page stack to variable
            var pageStack = Navigation.NavigationStack;

            // Previous page to variable
            var last = pageStack.ToArray();
            var lastCount = pageStack.Count();
            var lastPage = last[lastCount - 2];

            // If previous page is MainPage, ToMainPageBtn is not visible. Cancel-button handles the same thing
            if (lastPage.ToString() == "IsItMyTurn.MainPage")
            {
                ToMainPageBtn.IsVisible = false;
            }
            else
            {
                ToMainPageBtn.IsVisible = true;
            }
            //GetApartments();
        }

        //private async void GetApartments()
        //{
        //    HttpClient client = new HttpClient();
        //    HttpResponseMessage response = await client.GetAsync("https://isitmyturnapi.azurewebsites.net/api/apartment");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        string json = await response.Content.ReadAsStringAsync();
        //        Apartment[] apartmentObjectList = JsonConvert.DeserializeObject<Apartment[]>(json);
        //        Dictionary<int, string> apartmentDictionary = new Dictionary<int, string>();

        //        foreach (var item in apartmentObjectList)
        //        {
        //            apartmentDictionary.Add(item.ApartmentId, item.ApartmentName);
        //        }

        //        ApartmentPicker.ItemsSource = apartmentDictionary.ToList();
        //    }
        //    else
        //    {
        //        await DisplayAlert("Virhe", "Tapahtui odottamaton virhe. Ole hyvä ja käynnistä sovellus uudestaan.", "OK");
        //    }
        //}

        private void AddBtn_Clicked(object sender, EventArgs e)
        {
            PickerViewModel model = new PickerViewModel();
            ViewModels.Apartment item = (ViewModels.Apartment)ApartmentPicker.SelectedItem;
            DisplayAlert("Virhe", item.ApartmentId.ToString(), "OK");
            //Apartment apartment = new Apartment();
            //var item = apartment.SelectedItem;
            //DisplayAlert("Virhe", item.Key.ToString(), "OK");
            //NewShift shift = new NewShift()
            //{
            //    ApartmentId = ApartmentPicker.SelectedItem.
            //};

        }

        private void CancelBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void ToMainPageBtn_Clicked(object sender, EventArgs e)
        {
            
            Navigation.PopToRootAsync();
        }
    }
}