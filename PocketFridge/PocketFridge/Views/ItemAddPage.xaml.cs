using PocketFridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PocketFridge.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemAddPage : ContentPage
    {


        

        public DateTime currentDate
        {
            get { return DateTime.Now.Date; }
        }

        //The existing food item that can be selected
        public IList<FoodContainer> foodNames { get; private set; }
        public FoodContainer selected_foodContainer { private get; set; }

        // Below is the fields that needs to be populated for the foodItem
        public DateTime expiriyDate_ { get; set; }
        public string fridgeName_ { get; set; }
        public bool opened_ { get; set; }


        public ItemAddPage()
        {
            InitializeComponent();
            initFoodNames();
            BindingContext = this;
            
        }
        //Need a list of possible food names, in alphabetic order.
        private void initFoodNames()
        {
            foodNames = new List<FoodContainer>();
            var foodContainers = App.Database.GetAllFridgeItems();
            foreach (var container in foodContainers.OrderBy(item => item.foodName))
            {
                foodNames.Add(container);
            }
            
        }


        private void Cancle_Button_Clicked(object sender, EventArgs e)
        {
            exit_message();
        }

        protected override bool OnBackButtonPressed()
        {
            // Do your magic here
            exit_message();
            return true;
        }

        private async void exit_message()
        {
            var yes = await DisplayAlert("Discard changes", "Do you wish to exit adding food", "yes", "no");
            if (yes)
            {
                await Navigation.PopAsync();
            }
        }


        //Have fields that can be editet 
        private void Add_Button_Clicked(object sender, EventArgs e)
        {
            GetFoodContainerName();
        }

        /// <summary>
        /// This function will handle if the foodcontainer is new or not.
        /// </summary>
        private async void GetFoodContainerName()
        {
            FoodContainer container = null;
            var user_defined_foodName = entry_food_container.Text;
            //First check if a new name has been defined
            if (user_defined_foodName == null || user_defined_foodName.Trim() == "")
            {
                CheckExistingFoodContainer(ref container);
                if(container == null) {
                    await DisplayAlert("Input error", "You need to either pick and existing item, or define a new one", "Ok"); 
                    return; 
                }
                
            }
            else
            {
                //Check if the written name is actually an already defined item
                //For easier look-up convert list to dict, using the foodname as key
                var dict_food = foodNames.ToDictionary(x => x.foodName.ToLower());
                if (dict_food.ContainsKey(user_defined_foodName.ToLower()))
                {
                    //Alter the user
                    var yes = await DisplayAlert("Duplicate food name", "The food name {} already exist, do you want to use this container?", "yes", "no");
                    if (yes)
                    {
                        container = dict_food[user_defined_foodName.ToLower()];
                    }
                }
                
                if (container == null)
                {
                    container = new FoodContainer()
                    {
                        foodName = user_defined_foodName,
                        quantity = 0, //Define this as zero since we will increase the count as soon as we add a new food item
                        foods = new List<FoodItem>()
                    };
                }

                
            }

            // Now we know what container we use we can populate it with a new food item
            // First extract the other information given in the form
            Console.WriteLine("Food name: " + container.foodName);
            Console.WriteLine("Quantity: " + container.quantity);

            // Check if expiriy date has changed
            if(expiriyDate_ == currentDate)
            {
                //Exit function if the expiriy date needs to be changed
                if (!await DisplayAlert("expiriy date equals current date", "Are you sure that the expiriry date is today?", "yes", "no"))
                {
                    return;
                }

            }

            //Everything else is allowed to be null or default

            container.foods.Add(new FoodItem()
            {
                expiriyDate = expiriyDate_,
                fridgeName = fridgeName_,
                opened = opened_
            });
            container.quantity += 1;

            //Save the newly updated foodContainer
            App.Database.SaveItem(container);

            //Exit this page
            await Navigation.PopAsync();

        }


        private void CheckExistingFoodContainer(ref FoodContainer container)
        {
            try
            {
                container = selected_foodContainer;
            }
            catch (NullReferenceException)
            {
                DisplayAlert("Input error", "You need to either pick and existing item, or define a new one","Ok");
                container = null;
            }
            catch (Exception e)
            {
                DisplayAlert("Unhandled execption", e.ToString(), "Ok");
                container = null;
            }
        }

        //Later have barcoe scans, such that we can bind a specific barcode to a product name (foodName)


    }
}