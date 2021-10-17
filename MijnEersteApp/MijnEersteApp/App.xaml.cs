using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MijnEersteApp
{
    public partial class App : Application
    {
        public App()
        {
            DependencyService.RegisterSingleton<IDataStore<Creature>>(new RemoteCreatureStore());

            InitializeComponent();

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
