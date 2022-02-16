using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public Command<FoodItem> DeleteItemCommand { get; }
        public string foodName { get; private set; }
        public IList<FoodItem> foodDetail { get; private set; }
        private FoodContainer foodCon { get; set; }


        private string foodName_id;

        public ItemDetailPage(string foodName)
        {
            InitializeComponent();
            DeleteItemCommand = new Command<FoodItem>(OnDeleteItem);
            foodName_id = foodName;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Retrieve all the notes from the database, and set them as the
            // data source for the CollectionView.
            RefreshPage();


        }

        private async void RefreshPage()
        {
            try
            {
                // Retrieve the note and set it as the BindingContext of the page.
               
                foodCon = App.Database.GetFridgeItem(foodName_id);
                foodName = foodCon.foodName;
                foodDetail = foodCon.foods.OrderBy(x => x.expiriyDate).ToList();
                BindingContext = this;
            }
            catch (Exception e)
            {
                await DisplayAlert("Error loading object", $"There is no food object with the name {foodName_id}", "ok");
                Console.WriteLine(e);
                await Navigation.PopAsync();
            }
        }

        async void OnDeleteTapped(object sender, EventArgs args)
        {
            try
            {
                bool yes = await DisplayAlert("Delete", $"Are you sure you want to deleted the food item '{foodName_id}' and all its content?", "Yes", "No");
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

        private void OnDeleteItem(FoodItem item)
        {
            Console.WriteLine("hello");
            DisplayAlert("SWIPED", $"You did it! {item.expiriyString} {item.fridgeName} {item.opened}", "weee");
        }

        private void OnEditTapped(object sender, EventArgs e)
        {
            //var collView = sender as CollectionView;
            //FoodItem item = collView.SelectedItem as FoodItem;
            //Console.WriteLine(item.expiriyString);
        }

        private void SwipeItem_Invoked(SwipeItem sender, EventArgs e)
        {
            
            DisplayAlert("SWIPED", "You did it!", "sure?");
        }
    }

}