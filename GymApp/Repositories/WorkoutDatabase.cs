using System.Collections.Generic;
using System.Threading.Tasks;
using GymApp.Model;
using SQLite;

namespace GymApp.Repositories
{
    public class WorkoutDatabase
    {
        readonly SQLiteAsyncConnection database;

        public WorkoutDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Workout>().Wait();
        }

        public Task<List<Workout>> GetItemsAsync()
        {
            return database.Table<Workout>().ToListAsync();
        }

        public Task<Workout> GetItemAsync(string id)
        {
            return database.Table<Workout>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Workout item)
        {
            return database.InsertOrReplaceAsync(item);
        }

        public Task<int> DeleteItemAsync(Workout item)
        {
            return database.DeleteAsync(item);
        }
    }
}
