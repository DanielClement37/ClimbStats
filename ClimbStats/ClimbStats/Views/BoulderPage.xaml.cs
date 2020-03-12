using ClimbStats.Models;
using ClimbStats.Views.BoulderCrud;
using System;
using System.Collections.Generic;

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
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            List<Boulder> boulders = await App.BoulderVM.GetAllBoulders();

            lstBoulders.ItemsSource = boulders;
            lstBoulders.IsRefreshing = false;
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