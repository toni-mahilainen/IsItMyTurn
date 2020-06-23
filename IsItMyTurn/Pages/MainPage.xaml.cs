using IsItMyTurn.Models;
using IsItMyTurn.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IsItMyTurn
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
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
    }
}
