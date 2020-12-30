using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Exemplo3
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Main()) {
                BarBackgroundColor = Color.BurlyWood,
                BarTextColor = Color.White,
            };
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
