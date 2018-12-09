using System.Collections.Generic;
using System.Threading.Tasks;
using GymApp.Model;
using SQLite;

namespace GymApp.Repositories
{
    public class WorkoutTemplateDatabase
    {
        readonly SQLiteAsyncConnection database;

        public WorkoutTemplateDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<WorkoutTemplate>().Wait();
        }

        public Task<List<WorkoutTemplate>> GetItemsAsync()
        {
            return database.Table<WorkoutTemplate>().ToListAsync();
        }

        public Task<WorkoutTemplate> GetItemAsync(string id)
        {
            return database.Table<WorkoutTemplate>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(WorkoutTemplate item)
        {
            return database.InsertOrReplaceAsync(item);
        }

        public Task<int> DeleteItemAsync(WorkoutTemplate item)
        {
            return database.DeleteAsync(item);
        }
    }
}