using RandomApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RandomApp.Helpers
{
    public class DatabaseHelper
    {
        public static bool Insert<T>(ref T data)
        {
            using (var conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<T>();
                if (conn.Insert(data) != 0) return true;
            }
            return false;
        }

        public static List<Dish> Read()
        {
            List<Dish> dishList = new List<Dish>();
            using (var conn = new SQLite.SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<Dish>();
                dishList = conn.Table<Dish>().ToList();
            }
            return dishList;
        }
    }
}
