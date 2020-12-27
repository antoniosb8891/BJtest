using System;
using SQLite;

namespace BJtest.DB
{
    public class LocalDB
    {
        private readonly SQLiteAsyncConnection database;

        public LocalDB(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);
            database.EnableWriteAheadLoggingAsync().ConfigureAwait(false);
        }

    }
}
