using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BJtest.Models.DBModels;
using SQLite;

namespace BJtest.DB
{
    public class TaskTable
    {
        private readonly SQLiteAsyncConnection database;

        public TaskTable(SQLiteAsyncConnection db)
        {
            database = db;
            database.CreateTableAsync<TaskDBModel>().Wait();
        }

        public Task<List<TaskDBModel>> GetAllItemsAsync()
        {
            return database.Table<TaskDBModel>().ToListAsync();
        }

        public Task<TaskDBModel> GetItemByIdAsync(int id)
        {
            return database.Table<TaskDBModel>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> UpdateItemAsync(TaskDBModel item)
        {
            if (item.Id != 0)
                await database.UpdateAsync(item);
            return item.Id;
        }

        public async Task<int> InsertItemAsync(TaskDBModel item)
        {
            await database.InsertAsync(item);
            return item.Id;
        }

    }
}
