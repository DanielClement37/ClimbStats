using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClimbStats.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ClimbStats.ViewModels;

namespace ClimbStats.Views.SportCrud
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SportCreatePage : ContentPage
    {
        Dictionary<int, string> climbGrades = new Dictionary<int, string>
        {
            {0,"5.5" },{1,"5.6" },{2,"5.7" },{3,"5.8" },{4,"5.9" },
            {5,"5.10a" },{6,"5.10b" },{7,"5.10c" },{8,"5.10d" },
            {9,"5.11a" },{10,"5.11b" },{11,"5.11c" },{12,"5.11d" },
            {13,"5.12a" },{14,"5.12b" },{15,"5.12c" },{16,"5.12d" },
            {17,"5.13a" },{18,"5.13b" },{19,"5.13c" },{20,"5.13d" },
            {21,"5.14a" },{22,"5.14b" },{23,"5.14c" },{24,"5.14d" },
            {25,"5.15a" },{26,"5.15b" },{27,"5.15c" },{28,"5.15d" }
        };

        public SportCreatePage()
        {
            InitializeComponent();

            foreach(string grade in climbGrades.Values)
            {
                pkGrade.Items.Add(grade);
            }

        }

        private async void btnAdd_Clicked(object sender, EventArgs e)
        {
            var grade = climbGrades.SingleOrDefault(p => p.Key == pkGrade.SelectedIndex);
            int numAttempts = Convert.ToInt32(entNumAttempts.Text);
            var isOutdoors = cbIsOutdoors.IsChecked;
            
            await App.SportVM.AddSportClimb(numAttempts, grade, isOutdoors);

            await Navigation.PopAsync();
        }

        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}