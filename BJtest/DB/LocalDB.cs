using System;
using SQLite;

namespace BJtest.DB
{
    public class LocalDB
    {
        private readonly SQLiteAsyncConnection database;
        private readonly TaskTable taskTable;

        public LocalDB(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);
            database.EnableWriteAheadLoggingAsync().ConfigureAwait(false);
            taskTable = new TaskTable(database);
        }

        public TaskTable TaskTable
        {
            get => taskTable;
        }
    }
}
