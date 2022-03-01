﻿using PocketFridge.Models;
using PocketFridge.ViewModels;
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
        public EditFoodItem(FoodContainer container_, int foodItemIndex_)
        {
            InitializeComponent();
            BindingContext = new EditFoodItemViewModel(container_,foodItemIndex_) ;
        }

    }
}