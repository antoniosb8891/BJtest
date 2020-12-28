using System;
using SQLite;

namespace BJtest.Models.DBModels
{
    public class TaskDBModel
    {
        [PrimaryKey]
        public int Id { get; set; }
    }
}
