using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using PocketFridge.Models;

namespace PocketFridge.Views
{
    public partial class MainPage : ContentPage
    {

        public IList<FoodContainer> Inventory { get; private set; }

        public MainPage()
        {
            InitializeComponent();

            Inventory = new List<FoodContainer>
            {
                new FoodContainer
                {
                    foodName = "Milk",
                    foods = new List<FoodItem> { new FoodItem { expiriy = DateTime.Now.ToString(), fridgeName = "SeansMad", opened = false } ,
                              new FoodItem { expiriy = DateTime.Now.AddDays(-1).ToString(), fridgeName = "MichellesMad", opened = true } },
                    quantity = 2
                },

                new FoodContainer
                {
                    foodName = "Cheese",
                    foods = new List<FoodItem>  { new FoodItem { expiriy = DateTime.Now.AddDays(1).ToString(), fridgeName = "MichellesMad", opened = false } },
                    quantity = 1
                },

                new FoodContainer
                {
                    foodName = "Dogs",
                    foods = new List<FoodItem> { new FoodItem { expiriy = DateTime.Now.ToString(), fridgeName = "SeansMad", opened = false } },
                    quantity = 1
                }
            };

            for (int i = 0; i <= 10; i++)
            {
                Inventory.Add(
                    new FoodContainer
                    {
                        foodName = $"Dogs{i}",
                        foods = new List<FoodItem> { new FoodItem { expiriy = DateTime.Now.ToString(), fridgeName = "SeansMad", opened = false } },
                        quantity = 1
                    });
            }

            BindingContext = this;
            Console.WriteLine(Inventory[0].getOldest().fridgeName);
            

        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FoodContainer selectedItem = e.CurrentSelection[0] as FoodContainer;
            Console.WriteLine($"Selected item is: {selectedItem.foodName}, {selectedItem.quantity}");
            await Navigation.PushAsync(new ItemDetailPage("1"));
            
            
           
        }

        void OnCheckBoxChecked(object sender, CheckedChangedEventArgs e)
        {
            Console.WriteLine("Checkbox checked: " + e.Value);
        }
    }
}
