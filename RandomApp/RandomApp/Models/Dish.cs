using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace RandomApp.Models
{
    public class Dish
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
