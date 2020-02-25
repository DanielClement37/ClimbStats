using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClimbStats.Services;
using ClimbStats.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ClimbStats.ViewModels;

namespace ClimbStats.Views.SportCrud
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SportCreate : ContentPage
    {
        public SportCreate()
        {
            InitializeComponent();
            BindingContext = new SportViewModel();
        }

        private async void btnAdd_Clicked(object sender, EventArgs e)
        {
            var grade = entGrade.Text;
            int numAttempts = Convert.ToInt32(entNumAttempts.Text);
            var isOutdoors = cbIsOutdoors.IsChecked;

            SportViewModel sportViewModel = new SportViewModel();
            await sportViewModel.AddSportClimb(numAttempts, grade, isOutdoors);

            await Navigation.PopAsync();
        }

        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}