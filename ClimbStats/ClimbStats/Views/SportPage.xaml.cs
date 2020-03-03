using ClimbStats.Models;
using ClimbStats.Services;
using ClimbStats.ViewModels;
using ClimbStats.Views.SportCrud;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateXF.Widget.Charts.Models;
using UltimateXF.Widget.Charts.Models.Component;
using UltimateXF.Widget.Charts.Models.LineChart;
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

            lstSportClimbs.RefreshCommand = new Command(async () =>
            {
                List<SportClimb> sportClimbs = await App.SportVM.GetAllSportClimbs();

                lstSportClimbs.ItemsSource = sportClimbs;
                lstSportClimbs.IsRefreshing = false;

                UpdateDiffTimeGraph();
            });
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
            var entries = new List<EntryChart>();
            List<int> GradeInts = await App.SportVM.GetAllClimbInt();
            if (GradeInts.Count != 0)
            {
                int i = 1;
                foreach (int c in GradeInts)
                {
                    entries.Add(new EntryChart(i++, c));
                }

                var dataSet = new LineDataSetXF(entries, "Climb Difficulty Over Time")
                {
                    DrawCircleHole = false,
                    DrawValues = false,
                    CircleRadius = 1.5f,
                    FillAlpha = 0.2f,
                    DrawFilled = true,
                    LineWidth = 1.5f,
                };

                var lineChartData = new LineChartData(new List<ILineDataSetXF>() { dataSet });

                var xAxisConfig = new XAxisConfig()
                {
                    DrawGridLines = false,
                    XAXISPosition = XAXISPosition.BOTTOM,
                    AxisMinimum = 0
                };

                chart.XAxis = xAxisConfig;
                chart.AxisLeft.GridColor = Color.White;

                chart.AxisRight.DrawGridLinesBehindData = false;
                chart.AxisRight.Enabled = false;

                chart.AxisLeft.DrawGridLinesBehindData = false;
                chart.AxisLeft.Enabled = false;
                chart.AxisLeft.SpaceMin = 1.0f;
                chart.AxisLeft.SpaceMax = 1.0f;

                chart.DescriptionChart.Text = string.Empty;

                chart.Legend.Enabled = false;

                chart.ChartData = lineChartData;
            }
        }
    }
}