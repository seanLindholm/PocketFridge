using PocketFridge.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

        public Task<List<FoodContainer>> GetAllFridgeItems()
        {
            //Get all notes.
            return database.Table<FoodContainer>().ToListAsync();
        }

        public Task<FoodContainer> GetFridgeItem(int id)
        {
            // Get a specific note.
            return database.Table<FoodContainer>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveItem(FoodContainer item)
        {
            if (item.ID != 0)
            {
                // Update an existing note.
                return database.UpdateAsync(item);
            }
            else
            {
                // Save a new note.
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteNoteAsync(FoodContainer item)
        {
            // Delete a note.
            return database.DeleteAsync(item);
        }
    }
}
