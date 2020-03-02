using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        private void btnAdd_Clicked(object sender, EventArgs e)
        {
        }

        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}