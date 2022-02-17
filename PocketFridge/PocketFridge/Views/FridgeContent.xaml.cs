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

namespace PocketFridge.Views
{
    public partial class FridgeContent : ContentPage
    {
        public ICommand RefreshCommand { get; }
        bool isRefreshing;

        public bool IsRefreshing {
            get { return isRefreshing; }
            set
            {
                isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public ObservableCollection<FoodContainer> Inventory { get; private set; }



        public FridgeContent()
        {
            RefreshCommand = new Command(RefreshPage);
            InitializeComponent();
        }
       

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Retrieve all the notes from the database, and set them as the
            // data source for the CollectionView.
            RefreshPage();
            

        }
        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var collectionView = sender as CollectionView;
            if(collectionView.SelectedItem == null)
            {
                return;
            }
            FoodContainer selectedItem = e.CurrentSelection.FirstOrDefault() as FoodContainer;
            
            //When item selected navigate to the detail page
            await Navigation.PushAsync(new ItemDetailPage(selectedItem.foodName));

            //Clear selection
            collectionView.SelectedItem = null;
        }

        void OnCheckBoxChecked(object sender, CheckedChangedEventArgs e)
        {
            //Console.WriteLine("Checkbox checked: " + e.Value);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var foodItem = new FoodContainer
            {
                foodName = "Milk",
                foods = new List<FoodItem> { new FoodItem { expiriyDate = DateTime.Now, fridgeName = "SeansMad", opened = false } ,
                              new FoodItem { expiriyDate = DateTime.Now.AddDays(-1), fridgeName = "MichellesMad", opened = true } },
                quantity = 2
                
            };
            App.Database.SaveItem(foodItem);
            RefreshPage();
        }

        private async void OnAddTapped(object sender, EventArgs e)
        {

            //When item selected navigate to the detail page
            await Navigation.PushAsync(new ItemAddPage());
        }

        private void RefreshPage()
        {
            Inventory = App.Database.GetAllFridgeItems();
            OnPropertyChanged(nameof(Inventory));
            BindingContext = this;
            IsRefreshing = false;
        }
    }
}
