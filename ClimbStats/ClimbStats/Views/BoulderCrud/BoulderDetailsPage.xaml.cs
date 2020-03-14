using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClimbStats.Views.BoulderCrud
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BoulderDetailsPage : ContentPage
    {
        private Dictionary<int, string> climbGrades = new Dictionary<int, string>
        {
            {0, "VB" }, {1, "V0" }, {2, "V1" }, {3, "V2" },
            {4, "V3" }, {5, "V4" }, {6, "V5" }, {7, "V6" },
            {8, "V7" }, {9, "V8" }, {10, "V9" }, {11, "V10" },
            {12, "V11" }, {13, "V12" }, {14, "V13" }, {15, "V14" },
            {16, "V15" }, {17, "V16" }
        };

        public BoulderDetailsPage()
        {
            InitializeComponent();
            foreach (string grade in climbGrades.Values)
            {
                pkGrade.Items.Add(grade);
            }
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(lbId.Text);
            var grade = climbGrades.SingleOrDefault(p => p.Key == pkGrade.SelectedIndex);
            int numAttempts = Convert.ToInt32(entNumAttempts.Text);
            var isOutdoors = cbIsOutdoors.IsChecked;

            await App.BoulderVM.EditBoulder(id, numAttempts, grade, isOutdoors);
            await Navigation.PopAsync();
        }

        private async void btnDelete_Clicked(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(lbId.Text);
            await App.BoulderVM.DeleteBoulder(id);
            await Navigation.PopAsync();
        }

        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}