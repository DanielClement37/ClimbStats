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
            int id = Convert.ToInt32(lbId.Text);
            var grade = entGrade.Text;
            int numAttempts = Convert.ToInt32(entNumAttempts.Text);
            var isOutdoors = cbIsOutdoors.IsChecked;

            await App.SportVM.EditSportClimb(id,numAttempts,grade,isOutdoors);
            await Navigation.PopAsync();
        }

        private async void btnDelete_Clicked(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(lbId.Text);
            await App.SportVM.DeleteClimb(id);
            await Navigation.PopAsync();
        }

        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}