using PocketFridge.Models;
using PocketFridge.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PocketFridge.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemAddPage : ContentPage
    {


        ItemAddViewModel iavm = null; 

        public ItemAddPage(string foodName = null)
        {
            InitializeComponent();

            //If an input of foodname is set, sligthly modife the add page view
            if (foodName != null)
            {
                ExisitingFood.IsVisible = false;
                foodPicker.IsVisible = false;
                UserDefinedLable.Text = "Adding a food item for " + foodName; ;
                entry_food_container.IsVisible = false;
            }
            BindingContext = iavm = new ItemAddViewModel(foodName);

        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return iavm.OnBackButtonPressed();
        }

    }
}