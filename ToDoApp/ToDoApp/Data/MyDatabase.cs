using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;

namespace ToDoApp.Data
{
    internal static class MyDatabase
    {
        public static string DBNAME = "toDoData.db3";
        public static string database = Path.Combine(FileSystem.AppDataDirectory, DBNAME);
        public static string GetDatabasePath
        {
            get { return database; }
       
        }

        


    }
}
