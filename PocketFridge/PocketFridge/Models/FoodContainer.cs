using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketFridge.Models
{
    /// <summary>
    /// This class represent the food items located in the fridge
    /// For now all the information are stored in a local database 
    /// </summary>
    public class FoodContainer
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string foodName { get; set; }
        [TextBlob(nameof(foodsblobbed))]
        public List<FoodItem> foods { get; set; }
        public string foodsblobbed { get; set; }
        public int quantity { get; set; }

        public FoodItem oldest { 
            get { return getOldest(); } 
        }

        public FoodItem getOldest() 
        {
            FoodItem oldest = null;
            foreach (var item in foods)
            {
                if(oldest == null)
                {
                    oldest = item;
                }
                else if(oldest.expiriyDate > item.expiriyDate)
                {
                    oldest = item;
                }
            }
            return oldest;
        }

        public override string ToString()
        {
            return foodName;
        }
    }
}
