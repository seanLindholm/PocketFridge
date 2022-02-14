using PocketFridge.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLiteNetExtensions.Extensions;
using System.Collections.ObjectModel;

namespace PocketFridge.Data
{
    public class FoodDatabase
    {
        readonly SQLiteAsyncConnection database;

        public FoodDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<FoodContainer>().Wait();
        }

        public ObservableCollection<FoodContainer> GetAllFridgeItems()
        {
            ObservableCollection<FoodContainer> temp = new ObservableCollection<FoodContainer>();
            //Get all notes.
            foreach (var item in ReadOperations.GetAllWithChildren<FoodContainer>(database.GetConnection()))
            {
                temp.Add(item);
            }
            return temp;
        }

        public FoodContainer GetFridgeItem(string foodName_id)
        {
            FoodContainer result;
            //Try to find the food, by name
            try
            {
                result = ReadOperations.GetWithChildren<FoodContainer>(database.GetConnection(), foodName_id);
            }
            //If not just return null
            catch
            {
                result = null;
            }
            return result;
        }

        public void SaveItem(FoodContainer item)
        {
            //update the item if the foodName is not unique / already stored
            if (GetFridgeItem(item.foodName) != null)
            {
                // Update an existing note.
                WriteOperations.UpdateWithChildren(database.GetConnection(), item);
                Console.WriteLine("Updating " + item.foodName);
            }
            else
            {
                // Save a new note.
                WriteOperations.InsertWithChildren(database.GetConnection(), item);
                Console.WriteLine("Inserting " + item.foodName);

            }
        }

        public async Task<int> DeleteFoodContainer(FoodContainer item)
        {
            // Delete a note.
            var num_rows = await database.DeleteAsync(item);
            return num_rows;
        }
    }
}
