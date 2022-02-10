using System;
using System.Collections.Generic;
using System.Text;

namespace PocketFridge.Models
{
    public class FoodItem
    {
        internal DateTime expiriyDate;
        public string expiriy {
            get { return expiriyDate.ToString("dd/MM/yyyy"); }
            set { expiriyDate = DateTime.Parse(value); } 
        }

        public string fridgeName { get; set; }
        public bool opened { get; set; }

    }
}
