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
    public partial class EditFoodItem : ContentPage
    {

        private FoodContainer container;
        private int foodItemIndex;


        public FoodItem editable_item {get; set;}
        public DateTime currentDate {
            get   {    return DateTime.Now.Date; }
        }
        public EditFoodItem(FoodContainer container_, int foodItemIndex_)
        {
            container = container_;
            foodItemIndex = foodItemIndex_;

            editable_item = container.foods[foodItemIndex];
            Console.WriteLine(editable_item.ToString());
            InitializeComponent();
            BindingContext = this;
        }

        private async void Edit_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine(editable_item.ToString());
            container.foods[foodItemIndex] = editable_item;
            App.Database.SaveItem(container);
            await Navigation.PopAsync();


        }

        private async void Cancle_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}