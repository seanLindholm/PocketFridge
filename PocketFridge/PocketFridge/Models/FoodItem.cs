using System;
using System.Collections.Generic;
using System.Text;

namespace PocketFridge.Models
{
    public class FoodItem
    {
        public string expiriyString {
            get { return expiriyDate.ToString("dd/MM/yyyy"); }
        }
        public DateTime expiriyDate { get; set;} 
        public string fridgeName { get; set; }
        public bool opened { get; set; }
        public int? opened_dateTillExpired { get; set; }

    }
}
