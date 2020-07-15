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
        }

        private async void AddBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                // If Firebase Cloud Messaging token exists, new shift data will be sent to backend
                var FCMToken = Application.Current.Properties.Keys.Contains("Fcmtoken");
                if (FCMToken)
                {
                    Apartment item = (Apartment)ApartmentPicker.SelectedItem;

                    NewShift newShift = new NewShift()
                    {
                        ApartmentId = item.ApartmentId,
                        Date = DatePicker.Date
                    };

                    string json = JsonConvert.SerializeObject(newShift);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpClient client = new HttpClient();
                    HttpResponseMessage response = await client.PostAsync("https://isitmyturnapi.azurewebsites.net/api/completedshift", content);
                    int status = (int)response.StatusCode;
                    
                    // Status codes:
                    // 200 - Everything OK
                    // 201 - A shift has added successfully. Some problems with notifications
                    if (status == 200)
                    {
                        await DisplayAlert("Is It My Turn", "Kirjauksen lisäys onnistui!", "OK");
                        await Navigation.PopToRootAsync();
                    }
                    else if (status == 201)
                    {
                        await DisplayAlert("Is It My Turn",
                            "Kirjauksen lisäys onnistui, mutta ilmoitusten lähettämisessä käyttäjille ilmeni ongelmia.\r\n\r\n" +
                            "Käytä WhatsApp-ryhmää vuoron vaihdon ilmoittamiseen ja ota yhteyttä sovelluksen ylläpitäjään.", "OK");
                        await Navigation.PopToRootAsync();
                    }
                    else
                    {
                        await DisplayAlert("Is It My Turn", "Tapahtui virhe lisätessä kirjausta. Ole hyvä ja yritä uudelleen.\r\nJos ongelma ei poistu, ota yhteyttä sovelluksen ylläpitäjään.", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Is It My Turn", "Tapahtui virhe lisätessä kirjausta. Ole hyvä ja yritä uudelleen.\r\nJos ongelma ei poistu, ota yhteyttä sovelluksen ylläpitäjään.", "OK");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"######Error###### : {ex.Message}");
            }
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