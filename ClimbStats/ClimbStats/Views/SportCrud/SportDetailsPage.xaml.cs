using ClimbStats.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClimbStats.Views.SportCrud
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SportDetailsPage : ContentPage
    {
        public SportDetailsPage()
        {
            InitializeComponent();
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            var temp = new SportClimb();
            await App.SportVM.EditSportClimb(temp);
            await Navigation.PopAsync();
        }

        private async void btnDelete_Clicked(object sender, EventArgs e)
        {
            int id = 0;
            await App.SportVM.DeleteClimb(id);
            await Navigation.PopAsync();
        }

        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}