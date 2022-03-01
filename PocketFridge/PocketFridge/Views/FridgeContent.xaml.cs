using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using PocketFridge.Models;
using System.Windows.Input;
using System.Collections.ObjectModel;
using PocketFridge.ViewModels;

namespace PocketFridge.Views
{
    public partial class FridgeContent : ContentPage
    {
        private FridgeContentViewModel fridgeVM;


        public FridgeContent()
        {
            InitializeComponent();
            BindingContext = fridgeVM = new FridgeContentViewModel();
        }
       

        protected override void OnAppearing()
        {
            base.OnAppearing();
            fridgeVM.OnAppearing();
            

        }
    }
}
