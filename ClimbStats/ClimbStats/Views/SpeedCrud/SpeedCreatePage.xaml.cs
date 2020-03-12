using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClimbStats.Views.SpeedCrud
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SpeedCreatePage : ContentPage
    {
        public SpeedCreatePage()
        {
            InitializeComponent();
        }

        private void cbTopped_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (!cbTopped.IsChecked)
            {
                stTime.IsVisible = false;
            }
            else
            {
                stTime.IsVisible = true;
            }
        }

        private async void btnAdd_Clicked(object sender, EventArgs e)
        {
            var topped = cbTopped.IsChecked;
            double? time = null;

            if (topped)
            {
                time = Convert.ToDouble(enTime.Text);
                await App.SpeedVM.AddSpeedClimb(time, topped);
                await Navigation.PopAsync();
            }
            else
            {
                await App.SpeedVM.AddSpeedClimb(time, topped);
                await Navigation.PopAsync();
            }
        }

        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}