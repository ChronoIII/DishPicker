using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RandomApp
{
    public static class Constants
    {
        public const string DatabaseFilename = "DishPicker.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // Read & Write Permision
            SQLite.SQLiteOpenFlags.ReadWrite |
            // Create Database If not exists
            SQLite.SQLiteOpenFlags.Create |
            // Enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var BasePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(BasePath, DatabaseFilename);
            }
        }
    }
}
