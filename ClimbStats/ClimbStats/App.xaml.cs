using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ClimbStats.Views;
using ClimbStats.ViewModels;
using ClimbStats.Services;

namespace ClimbStats
{
    public partial class App : Application
    {
        private string dbPath => FileAccessHelper.GetLocalFilePath("Climbs.db");

        public static SportViewModel SportVM { get; private set; }

        public App()
        {
            InitializeComponent();

            SportVM = new SportViewModel(dbPath);

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}