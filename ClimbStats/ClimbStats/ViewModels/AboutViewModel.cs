using System;
using System.Windows.Input;
using ClimbStats.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ClimbStats.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://xamarin.com"));
        }

        public ICommand OpenWebCommand { get; }
    }
}