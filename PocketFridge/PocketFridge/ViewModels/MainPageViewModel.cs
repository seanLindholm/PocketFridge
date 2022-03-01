using PocketFridge.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PocketFridge.ViewModels
{
    public class MainPageViewModel
    {
        public ICommand goToFridgeContent { get; }


        public MainPageViewModel()
        {
            goToFridgeContent = new Command(GoToFridgeContent);
        }



        private void GoToFridgeContent()
        {
            Application.Current.MainPage.Navigation.PushAsync(new FridgeContent());
        }
    }
}
