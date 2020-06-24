using IsItMyTurn.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IsItMyTurn.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SeekAndDestroy : ContentPage
    {
        ObservableCollection<CompletedShifts> shiftList = new ObservableCollection<CompletedShifts>();

        public SeekAndDestroy()
        {
            InitializeComponent();
            
            shiftList.Add(new CompletedShifts { Apartment = "A1", Date = new DateTime(2020, 06, 24).ToString("dd.MM.yyyy") });
            shiftList.Add(new CompletedShifts { Apartment = "A2", Date = new DateTime(2020, 07, 24).ToString("dd.MM.yyyy") });
            shiftList.Add(new CompletedShifts { Apartment = "A4/B6", Date = new DateTime(2020, 08, 24).ToString("dd.MM.yyyy") });
            shiftList.Add(new CompletedShifts { Apartment = "B7", Date = new DateTime(2020, 09, 24).ToString("dd.MM.yyyy") });
            shiftList.Add(new CompletedShifts { Apartment = "B8", Date = new DateTime(2020, 10, 24).ToString("dd.MM.yyyy") });

            listView.ItemsSource = shiftList;
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
    }
}