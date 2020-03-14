using ClimbStats.Models;
using ClimbStats.Views.SportCrud;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
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

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            List<SportClimb> sportClimbs = await App.SportVM.GetAllSportClimbs();

            sportClimbs.Reverse();

            lstSportClimbs.ItemsSource = sportClimbs;
            lstSportClimbs.IsRefreshing = false;

            UpdateDiffTimeGraph();
            UpdateAttemptGradeGtaph();
            UpdateGradeCountGraph();
        }

        private async void OnSportAddClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SportCreatePage()
            {
                BindingContext = new SportClimb()
            });
        }

        private async void lstSportClimbs_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var data = lstSportClimbs.SelectedItem;
            await Navigation.PushAsync(new SportDetailsPage() { BindingContext = data });
        }

        private async void UpdateDiffTimeGraph()
        {
            var labels = await App.SportVM.GetAllClimbText();
            var data = await App.SportVM.GetAllClimbInt();

            var entries = new List<Microcharts.Entry>();

            for (int i = 0; i < data.Count; i++)
            {
                var index = i + 1;
                entries.Add(new Microcharts.Entry(data[i])
                {
                    Label = index.ToString(),
                    ValueLabel = labels[i],
                    Color = SKColor.Parse("#68B9C0")
                });
            }

            chDiffOverTime.Chart = new LineChart
            {
                Entries = entries,
                LineMode = LineMode.Straight,
                PointMode = PointMode.Square,
                BackgroundColor = SKColor.Empty,
                LabelTextSize = 35f
            };
        }

        private async void UpdateAttemptGradeGtaph()
        {
            var labels = await App.SportVM.GetGradesClimbed();
            var data = await App.SportVM.GetAvgNumAttempts();
            var entries = new List<Microcharts.Entry>();

            for (int i = 0; i < data.Count; i++)
            {
                entries.Add(new Microcharts.Entry((float)data[i])
                {
                    Label = labels[i],
                    ValueLabel = data[i].ToString(),
                    Color = SKColor.Parse("#68B9C0")
                });
            }

            chAttemptsPerGrade.Chart = new BarChart
            {
                Entries = entries,
                LabelTextSize = 35f,
                BackgroundColor = SKColor.Empty
            };
        }

        private async void UpdateGradeCountGraph()
        {
            var labels = await App.SportVM.GetGradesClimbed();
            var data = await App.SportVM.GetGradeCount();
            var entries = new List<Microcharts.Entry>();

            for (int i = 0; i < data.Count; i++)
            {
                entries.Add(new Microcharts.Entry((float)data[i])
                {
                    Label = labels[i],
                    ValueLabel = data[i].ToString(),
                    Color = SKColor.Parse("#68B9C0")
                });
            }

            chGradeCount.Chart = new BarChart
            {
                Entries = entries,
                LabelTextSize = 35f,
                BackgroundColor = SKColor.Empty
            };
        }
    }
}