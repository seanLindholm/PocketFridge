using PocketFridge.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PocketFridge.ViewModels
{
    class EditFoodItemViewModel : BindableObject
    {

        public ICommand CompleteEditCommand { get; }
        public ICommand CancleEditCommand { get; }

        private FoodContainer container;
        private int foodItemIndex;


        public FoodItem editable_item { get; set; }
        public DateTime currentDate
        {
            get { return DateTime.Now.Date; }
        }


        public EditFoodItemViewModel(FoodContainer container_, int foodItemIndex_)
        {
            CancleEditCommand = new Command(OnCancleClicked);
            CompleteEditCommand = new Command(OnEditComplete);

            container = container_;
            foodItemIndex = foodItemIndex_;
            editable_item = container.foods[foodItemIndex];
            BindingContext = this;
        }

        private void OnEditComplete()
        {
            Console.WriteLine(editable_item.ToString());
            container.foods[foodItemIndex] = editable_item;
            App.Database.SaveItem(container);
            Return();

        }

        private async void Return()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private void OnCancleClicked()
        {
            Return();
        }
    }
}
