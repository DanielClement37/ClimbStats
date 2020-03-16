using ClimbStats.Models;
using ClimbStats.Views.SpeedCrud;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;

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

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            List<SpeedClimb> sportClimbs = await App.SpeedVM.GetAllSpeedClimbs();

            sportClimbs.Reverse();

            lstSpeedClimbs.ItemsSource = sportClimbs;
            lstSpeedClimbs.IsRefreshing = false;

            var pb = await App.SpeedVM.GetFastest();
            lbBestTime.Text = pb.ToString();

            var avg = await App.SpeedVM.GetAverage();
            lbAvgTime.Text = avg.ToString();

            UpdateSpeedTimeGraph();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SpeedCreatePage()
            {
                BindingContext = new SpeedClimb()
            });
        }

        private async void lstSpeedClimbs_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var data = lstSpeedClimbs.SelectedItem;
            await Navigation.PushAsync(new SpeedDetailsPage() { BindingContext = data });
        }

        private async void UpdateSpeedTimeGraph()
        {
            var data = await App.SpeedVM.GetAllTImes();

            var entries = new List<Microcharts.Entry>();

            for (int i = 0; i < data.Count; i++)
            {
                var index = i + 1;
                entries.Add(new Microcharts.Entry((float)data[i])
                {
                    Label = index.ToString(),
                    ValueLabel = data[i].ToString(),
                    Color = SKColor.Parse("#68B9C0")
                });
            }

            chSpeedOverTime.Chart = new LineChart
            {
                Entries = entries,
                LineMode = LineMode.Straight,
                PointMode = PointMode.Square,
                BackgroundColor = SKColor.Empty,
                LabelTextSize = 35f
            };
            
        }
    }
}