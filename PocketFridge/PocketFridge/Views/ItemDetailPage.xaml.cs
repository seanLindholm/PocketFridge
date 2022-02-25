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
        public Command<FoodItem> ConsumeItemCommand { get; }
        public Command<FoodItem> EditItemCommand { get; }


        public string foodName { get; private set; }
        public ObservableCollection<FoodItem> foodDetail { get; private set; }
        private FoodContainer foodCon { get; set; }


        private string foodName_id;

        public ItemDetailPage(string foodName)
        {
            InitializeComponent();
            DeleteItemCommand = new Command<FoodItem>(OnDeleteItem);
            ConsumeItemCommand = new Command<FoodItem>(OnConsumedItem);
            EditItemCommand = new Command<FoodItem>(OnEditItem);
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
                foodDetail = new ObservableCollection<FoodItem>(foodCon.foods.OrderBy(x => x.expiriyDate).ToList());
                OnPropertyChanged(nameof(foodDetail));
                OnPropertyChanged(nameof(foodCon));
                BindingContext = this;
            }
            catch (Exception e)
            {
                await DisplayAlert("Error loading object", $"There is no food object with the name {foodName_id}", "ok");
                Console.WriteLine(e);
                await Navigation.PopAsync();
            }
        }

        private async void OnDeleteTapped(object sender, EventArgs args)
        {
            try
            {
                bool yes = await DisplayAlert("Delete", $"Are you sure you want to deleted the food item '{foodName_id}' and all its content?", "Yes", "No");
                if (yes)
                {
                    await App.Database.DeleteFoodContainer(foodCon);
                    await Navigation.PopAsync();
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async void OnAddTapped(object sender, EventArgs e)
        {
            //When item selected navigate to the detail page
            await Navigation.PushAsync(new ItemAddPage(foodName));
        }

        private async void OnDeleteItem(FoodItem item)
        {
            if(await DisplayAlert("Delete item from fridge", $"Are you sure you want to delete this item?", "yes", "no"))
            {
                if(await DisplayAlert("Delete item from fridge", $"Are you deleting item because it is getting thrown out?", "yes", "no"))
                {
                    // Add wasted item to counter (statistics)
                }

                deleteFoodItemFromCollection(item);
            }
        }

        private void deleteFoodItemFromCollection(FoodItem item)
        {
            //When deleting remove this from the collectionView list, and the actual foodContainer
            foodDetail.Remove(item);
            foodCon.foods.Remove(item);
            foodCon.quantity -= 1;
            OnPropertyChanged(nameof(foodDetail));
            OnPropertyChanged(nameof(foodCon));

            //Save the updated food container
            App.Database.SaveItem(foodCon);
        }

        private async void OnConsumedItem(FoodItem item)
        {
            if (await DisplayAlert("Consumed item", $"Do you wish to mark this item as consumed?", "yes", "no"))
            {
                deleteFoodItemFromCollection(item);
            }
        }

        private void OnEditItem(FoodItem item)
        {
            Console.WriteLine("Edit this item!");
            var foodItemIndex = foodCon.foods.FindIndex(x => x.Equals(item));
            Console.WriteLine(foodItemIndex);
            Console.WriteLine($"food item: {foodCon.foods[foodItemIndex].expiriyString} {foodCon.foods[foodItemIndex].opened} {foodCon.foods[foodItemIndex].fridgeName}");
            
            Navigation.PushAsync(new EditFoodItem(foodCon,foodItemIndex));

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