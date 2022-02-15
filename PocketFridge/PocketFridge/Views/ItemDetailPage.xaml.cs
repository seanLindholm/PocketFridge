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

        public ItemDetailPage(string foodName = null)
        {
            InitializeComponent();
            if (foodName != null)
            {
                LoadItem(foodName);
            }
        }

        void LoadItem(string foodName)
        {
            try
            {
                
                // Retrieve the note and set it as the BindingContext of the page.
                foodCon = App.Database.GetFridgeItem(foodName);
                this.foodName = foodCon.foodName;
                foodDetail = foodCon.foods.OrderBy(x=>x.expiriyDate).ToList();
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
                bool yes = await DisplayAlert("Delete", $"Are you sure you want to deleted the food item '{foodName}' and all its content?", "Yes", "No");
                if (yes)
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