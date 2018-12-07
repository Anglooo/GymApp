using System.Collections.Generic;
using System.Threading.Tasks;
using GymApp.Model;
using SQLite;

namespace GymApp.Repositories
{
    public class ExcersizeDatabase
    {
        readonly SQLiteAsyncConnection database;

        public ExcersizeDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Excersize>().Wait();
        }

        public Task<List<Excersize>> GetItemsAsync()
        {
            return database.Table<Excersize>().ToListAsync();
        }

        public Task<Excersize> GetItemAsync(string id)
        {
            return database.Table<Excersize>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Excersize item)
        {
            return database.InsertOrReplaceAsync(item);
        }

        public Task<int> DeleteItemAsync(Excersize item)
        {
            return database.DeleteAsync(item);
        }
    }
}
