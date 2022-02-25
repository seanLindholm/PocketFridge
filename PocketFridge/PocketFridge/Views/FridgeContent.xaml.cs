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
            fridgeVM = new FridgeContentViewModel(Navigation);
            InitializeComponent();
            BindingContext = fridgeVM;
        }
       

        protected override void OnAppearing()
        {
            base.OnAppearing();
            fridgeVM.RefreshPage();
            

        }
        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var collectionView = sender as CollectionView;
            if (collectionView.SelectedItem == null)
            {
                return;
            }
            FoodContainer selectedItem = e.CurrentSelection.FirstOrDefault() as FoodContainer;

            //When item selected navigate to the detail page
            await Navigation.PushAsync(new ItemDetailPage(selectedItem.foodName));

            //Clear selection
            collectionView.SelectedItem = null;
        }
    }
}
