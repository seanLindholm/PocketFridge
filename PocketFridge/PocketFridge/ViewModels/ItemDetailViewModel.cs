using PocketFridge.Models;
using PocketFridge.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace PocketFridge.ViewModels
{
    class ItemDetailViewModel : BindableObject
    {
        public Command<FoodItem> DeleteItemCommand { get; }
        public Command<FoodItem> ConsumeItemCommand { get; }
        public Command<FoodItem> EditItemCommand { get; }


        public Command DeleteContainerCommand { get; }
        public Command AddToContainerCommand { get; }



        public string foodName { get; private set; }
        public ObservableCollection<FoodItem> foodDetail { get; private set; }
        private FoodContainer foodCon { get; set; }


        private string foodName_id;

        public ItemDetailViewModel(string foodName)
        {
            DeleteItemCommand = new Command<FoodItem>(OnDeleteItem);
            ConsumeItemCommand = new Command<FoodItem>(OnConsumedItem);
            EditItemCommand = new Command<FoodItem>(OnEditItem);
            DeleteContainerCommand = new Command(OnDeleteTapped);
            AddToContainerCommand = new Command(OnAddTapped);
            foodName_id = foodName;
        }


        public void OnAppearing()
        {
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
                await Application.Current.MainPage.DisplayAlert("Error loading object", $"There is no food object with the name {foodName_id}", "ok");
                Console.WriteLine(e);
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }

        private async void OnDeleteTapped()
        {
            try
            {
                bool yes = await Application.Current.MainPage.DisplayAlert("Delete", $"Are you sure you want to deleted the food item '{foodName_id}' and all its content?", "Yes", "No");
                if (yes)
                {
                    await App.Database.DeleteFoodContainer(foodCon);
                    await Application.Current.MainPage.Navigation.PopAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        private async void OnAddTapped()
        {
            //When item selected navigate to the detail page
            await Application.Current.MainPage.Navigation.PushAsync(new ItemAddPage(foodName));
        }

        private async void OnDeleteItem(FoodItem item)
        {
            if (await Application.Current.MainPage.DisplayAlert("Delete item from fridge", $"Are you sure you want to delete this item?", "yes", "no"))
            {
                if (await Application.Current.MainPage.DisplayAlert("Delete item from fridge", $"Are you deleting item because it is getting thrown out?", "yes", "no"))
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
            if (await Application.Current.MainPage.DisplayAlert("Consumed item", $"Do you wish to mark this item as consumed?", "yes", "no"))
            {
                deleteFoodItemFromCollection(item);
            }
        }

        private void OnEditItem(FoodItem item)
        {
            Console.WriteLine("Edit me!");
            var foodItemIndex = foodCon.foods.FindIndex(x => x.Equals(item));
            Application.Current.MainPage.Navigation.PushAsync(new EditFoodItem(foodCon, foodItemIndex));

        }

    }
}

