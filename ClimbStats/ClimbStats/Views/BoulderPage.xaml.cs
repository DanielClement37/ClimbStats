using ClimbStats.Models;
using ClimbStats.Views.BoulderCrud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClimbStats.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BoulderPage : ContentPage
    {
        public BoulderPage()
        {
            InitializeComponent();

            lstBoulders.RefreshCommand = new Command(async () =>
            {
                //Do your stuff.
                List<Boulder> sportClimbs = await App.BoulderVM.GetAllBoulders();

                lstBoulders.ItemsSource = sportClimbs;
                lstBoulders.IsRefreshing = false;
            });
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BoulderCreatePage()
            {
                BindingContext = new Boulder()
            });
        }

        private async void lstBoulders_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var data = lstBoulders.SelectedItem;
            await Navigation.PushAsync(new BoulderDetailsPage() { BindingContext = data });
        }
    }
}