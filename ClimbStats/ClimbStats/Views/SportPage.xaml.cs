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



            lstSportClimbs.RefreshCommand = new Command(async () => {
                //Do your stuff.    
                List<SportClimb> sportClimbs = await App.SportVM.GetAllSportClimbs();

                lstSportClimbs.ItemsSource = sportClimbs;
                lstSportClimbs.IsRefreshing = false;
            });

            //demo graph for now
            //todo make a new endpoint in View model and event to update entries
            var entries = new List<EntryChart>();

            for (int i = 0; i < 20; i++)
            {
                entries.Add(new EntryChart(i, 2.00f));
            }

            var dataSet = new LineDataSetXF(entries, "Daily Sales")
            {
                //Colors = new List<Color> { App.GetAppResourceColorByKey("NetoDarkBlue") },
                //CircleColors = new List<Color> { App.GetAppResourceColorByKey("NetoDarkBlue") },
                DrawCircleHole = false,
                DrawValues = false,
                CircleRadius = 1.5f,
                //GradientColor = new GradientColor(App.GetAppResourceColorByKey("NetoDarkBlue"), App.GetAppResourceColorByKey("NetoLightBlue")),
                FillAlpha = 0.2f,
                DrawFilled = true,
                LineWidth = 1.5f,
                CubicIntensity = 10.0f,
                Mode = LineDataSetMode.CUBIC_BEZIER
            };

            var lineChartData = new LineChartData(new List<ILineDataSetXF>() { dataSet });

            var xAxisConfig = new XAxisConfig()
            {
                DrawGridLines = false,
                XAXISPosition = XAXISPosition.BOTTOM
            };

            chart.AnimationX = new AnimatorXF()
            {
                Duration = 5,
                EasingType = EasingOptionXF.EaseInOutBounce
            };

            chart.XAxis = xAxisConfig;
            chart.AxisLeft.GridColor = Color.White;

            chart.AxisRight.DrawGridLinesBehindData = false;
            chart.AxisRight.Enabled = false;

            chart.AxisLeft.DrawGridLinesBehindData = false;
            chart.AxisLeft.Enabled = false;

            chart.DescriptionChart.Text = string.Empty;

            chart.Legend.Enabled = false;

            chart.ChartData = lineChartData;
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
    }
}