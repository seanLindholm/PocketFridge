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

        public FoodContainer GetFridgeItem(int id)
        {
            // Get a specific note.
            return ReadOperations.GetWithChildren<FoodContainer>(database.GetConnection(), id);
            //return database.Table<FoodContainer>()
            //                .Where(i => i.ID == id)
            //                .FirstOrDefaultAsync();
        }

        public void SaveItem(FoodContainer item)
        {
            if (item.ID != 0)
            {
                // Update an existing note.
                WriteOperations.UpdateWithChildren(database.GetConnection(), item);
            }
            else
            {
                // Save a new note.
                WriteOperations.InsertWithChildren(database.GetConnection(), item);
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
