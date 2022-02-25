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

        private INavigation navigation;

        public MainPageViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            goToFridgeContent = new Command(GoToFridgeContent);
        }



        private void GoToFridgeContent()
        {
            navigation.PushAsync(new FridgeContent());
        }
    }
}
