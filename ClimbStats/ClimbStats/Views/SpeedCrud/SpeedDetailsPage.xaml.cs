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


        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            var topped = cbTopped.IsChecked;
            int id = Convert.ToInt32(lbId.Text);
            double? time = null;

            if (topped)
            {
                time = Convert.ToDouble(enTime.Text);
                await App.SpeedVM.EditSpeedClimb(id,time, topped);
                await Navigation.PopAsync();
            }
            else
            {
                await App.SpeedVM.AddSpeedClimb(time, topped);
                await Navigation.PopAsync();
            }
        }

        private async void btnDelete_Clicked(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(lbId.Text);
            await App.SpeedVM.DeleteClimb(id);
            await Navigation.PopAsync();
        }

        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}