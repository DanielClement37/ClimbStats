using ClimbStats.Services;
using ClimbStats.ViewModels;
using ClimbStats.Views;
using Xamarin.Forms;

namespace ClimbStats
{
    public partial class App : Application
    {
        private string dbPath => FileAccessHelper.GetLocalFilePath("Climbs.db");

        public static SportViewModel SportVM { get; private set; }
        public static BoulderViewModel BoulderVM { get; private set; }
        public static SpeedViewModel SpeedVM { get; private set; }

        public App()
        {
            InitializeComponent();

            SportVM = new SportViewModel(dbPath);
            BoulderVM = new BoulderViewModel(dbPath);
            SpeedVM = new SpeedViewModel(dbPath);

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