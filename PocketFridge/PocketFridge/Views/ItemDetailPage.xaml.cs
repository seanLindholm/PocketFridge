using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PocketFridge.Models;
using PocketFridge.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PocketFridge.Views
{

    public partial class ItemDetailPage : ContentPage
    {

        private ItemDetailViewModel ItemDetailViewModel_;

        public ItemDetailPage(string foodName)
        {
            InitializeComponent();
            BindingContext = ItemDetailViewModel_ = new ItemDetailViewModel(foodName);
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Retrieve all the notes from the database, and set them as the
            // data source for the CollectionView.
            ItemDetailViewModel_.OnAppearing();


        }
    }

}