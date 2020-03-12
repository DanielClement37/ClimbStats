using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClimbStats.Views.SpeedCrud
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SpeedDetailsPage : ContentPage
    {
        public SpeedDetailsPage()
        {
            InitializeComponent();
        }

        private void cbTopped_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
        }

        private async void btnDelete_Clicked(object sender, EventArgs e)
        {
        }

        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}