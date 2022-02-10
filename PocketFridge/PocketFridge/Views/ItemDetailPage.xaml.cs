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

        public ItemDetailPage(string ItemId = null)
        {
            InitializeComponent();
            if (ItemId != null)
            {
                LoadItem(ItemId);
            }
        }

        async void LoadItem(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                // Retrieve the note and set it as the BindingContext of the page.
                FoodContainer item = await App.Database.GetFridgeItem(id);
                BindingContext = item;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load note.");
            }
        }

        void OnBtnClicked(object sender, EventArgs args)
        {
            Console.WriteLine("Thank you");
            Navigation.PopAsync();
        }
    }

}