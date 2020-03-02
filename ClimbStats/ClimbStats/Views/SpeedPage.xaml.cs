using ClimbStats.Models;
using ClimbStats.Views.SpeedCrud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClimbStats.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SpeedPage : ContentPage
    {
        public SpeedPage()
        {
            InitializeComponent();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SpeedCreatePage()
            {
                BindingContext = new SpeedClimb()
            });
        }

        private void lstSpeedClimbs_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
        }
    }
}