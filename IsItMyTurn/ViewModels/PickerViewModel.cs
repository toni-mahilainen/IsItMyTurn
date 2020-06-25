using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IsItMyTurn.ViewModels
{
    public class PickerViewModel
    {
        public Task<List<Apartment>> ApartmentList { get; set; }
        public PickerViewModel()
        {
            ApartmentList = GetApartments();
        }
        private async Task<List<Apartment>> GetApartments()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://isitmyturnapi.azurewebsites.net/api/apartment");

            string json = await response.Content.ReadAsStringAsync();
            Apartment[] apartmentObjectList = JsonConvert.DeserializeObject<Apartment[]>(json);
            List<Apartment> apartments = apartmentObjectList.ToList();

            return apartments;
        }
    }

    public class Apartment
    {
        public int ApartmentId { get; set; }
        public string ApartmentName { get; set; }
    }
}
