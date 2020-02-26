using ClimbStats.Models;
using ClimbStats.Services;
using ClimbStats.ViewModels;
using ClimbStats.Views.SportCrud;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClimbStats.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SportPage : ContentPage
    {
        public SportPage()
        {
            InitializeComponent();
        }

        private async void OnSportAddClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SportCreatePage()
            {
                BindingContext = new SportClimb()
            });
        }

        private async void OnGetAllClicked(object sender, EventArgs e)
        {
            List<SportClimb> sportClimbs = await App.SportVM.GetAllSportClimbs();

            lstSportClimbs.ItemsSource = sportClimbs;
        }

        private async void lstSportClimbs_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var data = lstSportClimbs.SelectedItem;
            await Navigation.PushAsync(new SportDetailsPage() { BindingContext = data });
        }
    }
}