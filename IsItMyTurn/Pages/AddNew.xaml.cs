using System;
using System.Collections.Generic;
using System.Linq;
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
        }

        private void AddBtn_Clicked(object sender, EventArgs e)
        {

        }

        private void CancelBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PopToRootAsync();
        }
    }
}