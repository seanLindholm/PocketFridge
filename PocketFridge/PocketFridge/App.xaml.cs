using System;
using Xamarin.Forms;
using PocketFridge.Data;
using Xamarin.Forms.Xaml;
using System.IO;
using PocketFridge.Views;

namespace PocketFridge
{
    public partial class App : Application
    {
        static FoodDatabase database;

        // Create the database connection as a singleton.
        public static FoodDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new FoodDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Food.db3"));
                }
                return database;
            }
        }
         
        public App()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            MainPage = new NavigationPage(new MainPage());
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
