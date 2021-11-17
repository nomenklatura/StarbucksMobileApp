using System.Threading;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StarbucksMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PgSplash : ContentPage
    {
        public PgSplash()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Thread.Sleep(3000);
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }
    }
}