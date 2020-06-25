using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IsItMyTurn.ViewModels
{
    public class PickerViewModel
    {
        public List<Apartment> ApartmentList { get; set; }
        public PickerViewModel()
        {
            ApartmentList = GetApartments();
        }
        public List<Apartment> GetApartments()
        {
            //HttpClient client = new HttpClient();
            //HttpResponseMessage response = await client.GetAsync("https://isitmyturnapi.azurewebsites.net/api/apartment");

            //string json = await response.Content.ReadAsStringAsync();
            //Apartment[] apartmentObjectList = JsonConvert.DeserializeObject<Apartment[]>(json);
            //List<Apartment> apartments = apartmentObjectList.ToList();
            var apartments = new List<Apartment>
            {
                new Apartment() { ApartmentId = 1, ApartmentName = "A1"},
                new Apartment() { ApartmentId = 2, ApartmentName = "A2"},
                new Apartment() { ApartmentId = 3, ApartmentName = "A4/B6"},
                new Apartment() { ApartmentId = 4, ApartmentName = "B7"},
                new Apartment() { ApartmentId = 5, ApartmentName = "B8"}
            };

            return apartments;
        }

        private KeyValuePair<int, string> _selectedItem;

        public KeyValuePair<int, string> SelectedItem
        {
            get => _selectedItem;
            set => _selectedItem = value;
        }
    }

    public class Apartment
    {
        public int ApartmentId { get; set; }
        public string ApartmentName { get; set; }
    }
}
