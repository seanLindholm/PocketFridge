using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PocketFridge.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PocketFridge.Views
{

    public partial class ItemDetailPage : ContentPage
    {

        public string foodName { get; private set; }
        public IList<FoodItem> foodDetail { get; private set; }
        private FoodContainer foodCon { get; set; }

        public ItemDetailPage(int? ItemId = null)
        {
            InitializeComponent();
            if (ItemId != null)
            {
                LoadItem((int)ItemId);
            }
        }

        void LoadItem(int itemId)
        {
            try
            {
                
                // Retrieve the note and set it as the BindingContext of the page.
                foodCon = App.Database.GetFridgeItem(itemId);
                foodName = foodCon.foodName;
                foodDetail = foodCon.foods;
                BindingContext = this;
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to load note.");
                Console.WriteLine(e);

            }
        }

       
        async void OnDeleteTapped(object sender, EventArgs args)
        {
            try
            {
                bool answer = await DisplayAlert("Delete", $"Are you sure you want to deleted food item {foodName} and all its content?", "Yes", "No");
                Console.WriteLine("Answer: " + answer);
                if (answer)
                {
                    await App.Database.DeleteFoodContainer(foodCon);
                    await Navigation.PopToRootAsync();
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }

}