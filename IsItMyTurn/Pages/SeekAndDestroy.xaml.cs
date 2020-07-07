using IsItMyTurn.Models;
using IsItMyTurn.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IsItMyTurn.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SeekAndDestroy : ContentPage
    {
        ObservableCollection<CompletedShift> shiftList = new ObservableCollection<CompletedShift>();

        public SeekAndDestroy()
        {
            InitializeComponent();
            BindingContext = new SeekAndDestroyListViewModel();
        }

        protected override void OnAppearing()
        {
            GetCompletedShifts();
        }

        private async void GetCompletedShifts()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://isitmyturnapi.azurewebsites.net/api/completedshift");
            
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                List<CompletedShifts> completedShiftsList = JsonConvert.DeserializeObject<List<CompletedShifts>>(json);
                List<CompletedShift> newCompeletedShiftsList = new List<CompletedShift>();

                foreach (var item in completedShiftsList)
                {
                    CompletedShift newListItem = new CompletedShift() 
                    { 
                        ShiftId = item.ShiftId, 
                        ApartmentId = item.ApartmentId, 
                        ApartmentName = item.ApartmentName, 
                        Date = item.Date.ToString("dd.MM.yyyy") 
                    };

                    newCompeletedShiftsList.Add(newListItem);
                }

                SeekAndDestroyListViewModel model = new SeekAndDestroyListViewModel()
                {
                    CompletedShiftList = newCompeletedShiftsList
                };

                listView.ItemsSource = newCompeletedShiftsList;
            }
            else
            {
                await DisplayAlert("Virhe", "Kirjausten haku epäonnistui! Ole hyvä ja lataa sivu uudelleen.", "OK");
            }
        }

        private void AddNewBtn_Clicked(object sender, EventArgs e)
        {
            var addNewPage = new AddNew();
            NavigationPage.SetHasNavigationBar(addNewPage, false);
            Navigation.PushAsync(addNewPage);
        }

        private void ToMainPageBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PopToRootAsync();
        }

        private void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            DeleteShiftBtn.IsEnabled = true;
            EditShiftBtn.IsEnabled = true;
        }

        private async void DeleteShiftBtn_Clicked(object sender, EventArgs e)
        {
            CompletedShift item = (CompletedShift)listView.SelectedItem;
            
            var answer = await DisplayAlert("Is It My Turn", 
                "Haluatko varmasti poistaa valitun kirjauksen?\r\n\r\n" +
                "Asunto: " + item.ApartmentName + "\r\n" +
                "Leikkuu ajankohta: " + item.Date, "Kyllä", "Ei");
            if (answer)
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.DeleteAsync("https://isitmyturnapi.azurewebsites.net/api/completedshift/" + item.ShiftId.ToString());

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Is It My Turn", "Kirjaus poistettu onnistuneesti!", "OK");
                    var vUpdatedPage = new SeekAndDestroy();
                    Navigation.InsertPageBefore(vUpdatedPage, this);
                    NavigationPage.SetHasNavigationBar(vUpdatedPage, false);
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Virhe", "Kirjauksen poisto epäonnistui! Ole hyvä ja yritä uudelleen.", "OK");
                }
            }
        }

        private void EditShiftBtn_Clicked(object sender, EventArgs e)
        {
            CompletedShift item = (CompletedShift)listView.SelectedItem;

            var editPage = new EditPage(item);
            Navigation.PushAsync(editPage);
        }
    }
}