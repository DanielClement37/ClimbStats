using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClimbStats.Views.BoulderCrud
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BoulderCreatePage : ContentPage
    {
        public BoulderCreatePage()
        {
            InitializeComponent();
        }

        private async void btnAdd_Clicked(object sender, EventArgs e)
        {
            var grade = pkGrade.SelectedItem.ToString();
            int numAttempts = Convert.ToInt32(entNumAttempts.Text);
            var isOutdoors = cbIsOutdoors.IsChecked;

            await App.BoulderVM.AddBoulder(numAttempts, grade, isOutdoors);

            await Navigation.PopAsync();
        }

        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}