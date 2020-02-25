using ClimbStats.Models;
using ClimbStats.ViewModels;
using ClimbStats.Views.SportCrud;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClimbStats.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SportPage : ContentPage
    {
        public SportPage()
        {
            InitializeComponent();
        }

        async void OnSportAddClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SportCreate
            {
                BindingContext = new SportClimb()
            });
        }

        async void OnGetAllClicked(object sender, EventArgs e)
        {
            SportViewModel svm = new SportViewModel();
            List<SportClimb> sportClimbs = await svm.GetAllSportClimbs();

            ClimbList = new ObservableCollection<string>();

            foreach(SportClimb c in sportClimbs)
            {
                ClimbList.Add(c.ToString());
            }

            lstSportClimbs.ItemsSource = ClimbList;
        }

        public void OnMore(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("More Context Action", mi.CommandParameter + " more context action", "OK");
        }

        ObservableCollection<string> ClimbList;
    }
}