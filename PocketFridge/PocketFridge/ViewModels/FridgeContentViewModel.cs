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
using PocketFridge.Views;

namespace PocketFridge.ViewModels
{
    class FridgeContentViewModel : BindableObject
    {

        public ICommand RefreshCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand ConsumeCommand { get; }

        bool isRefreshing;

        private INavigation navigation;
        

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set
            {
                isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public ObservableCollection<FoodContainer> Inventory { get; private set; }



        public FridgeContentViewModel(INavigation navigation)
        {
            RefreshCommand = new Command(RefreshPage);
            AddCommand = new Command(OnAddTapped);
            ConsumeCommand = new Command<FoodContainer>(consumeOldest);
            this.navigation = navigation;
        }

       
        private async void OnAddTapped()
        {
            //When item selected navigate to the detail page
            await Application.Current.MainPage.Navigation.PushAsync(new ItemAddPage());
        }

        public void RefreshPage()
        {
            Inventory = new ObservableCollection<FoodContainer>(App.Database.GetAllFridgeItems().Where(x => x.oldest != null) );
            OnPropertyChanged(nameof(Inventory));
            BindingContext = this;
            IsRefreshing = false;
        }




        private async void consumeOldest(FoodContainer item)
        {
            //If this command is activated the user will be notified if this is the wanted solution
            if (await Application.Current.MainPage.DisplayAlert("Consumed item", $"Do you wish consume the oldest product?", "yes", "no"))
            {
                //When deleting remove this from the collectionView list, and the actual foodContainer
                item.foods.Remove(item.oldest);
                item.quantity -= 1;
                //Save the updated food container
                App.Database.SaveItem(item);

                RefreshPage();
            }
            

        }




    }
}
