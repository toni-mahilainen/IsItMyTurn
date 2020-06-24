using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IsItMyTurn.Models
{
    class Apartment
    {
        public int ApartmentId { get; set; }
        public string ApartmentName { get; set; }
        
        public List<KeyValuePair<int, string>> PickerItemList { get; set; }

        private KeyValuePair<int, string> _selectedItem;
        public KeyValuePair<int, string> SelectedItem
        {
            get => _selectedItem;
            set => _selectedItem = value;
        }
    }
}
