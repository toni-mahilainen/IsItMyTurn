using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IsItMyTurn.ViewModels
{
    public class PickerViewModel : INotifyPropertyChanged
    {
        public List<Apartment> ApartmentList { get; set; }
        public PickerViewModel()
        {
            ApartmentList = GetApartments();
        }
        public List<Apartment> GetApartments()
        {
            // Apartments for picker
            var apartments = new List<Apartment>
            {
                new Apartment() { ApartmentId = 1, ApartmentName = "A1"},
                new Apartment() { ApartmentId = 2, ApartmentName = "A2"},
                new Apartment() { ApartmentId = 3, ApartmentName = "A4 / B6 1"},
                new Apartment() { ApartmentId = 4, ApartmentName = "A4 / B6 2"},
                new Apartment() { ApartmentId = 5, ApartmentName = "B7"},
                new Apartment() { ApartmentId = 6, ApartmentName = "B8"}
            };

            return apartments;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private Apartment _selectedItem;

        public Apartment SelectedItem
        {
            get => _selectedItem;
            set 
            { 
                _selectedItem = value;
                OnPropertyChanged();
            }
        }
    }

    public class Apartment
    {
        public int ApartmentId { get; set; }
        public string ApartmentName { get; set; }
    }
}
