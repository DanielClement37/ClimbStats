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

        private async void btnAdd_Clicked(object sender, EventArgs e)
        {
            double time;

              time = Convert.ToDouble(enTime.Text);
                await App.SpeedVM.AddSpeedClimb(time);
                await Navigation.PopAsync();
            
        }

        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}