using System;
using System.IO;
using ToDoApp.Data;
using ToDoApp.Model;
using ToDoApp.View;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoApp
{
    public partial class App : Application
    {
        public static string DBNAME = "Mytododata.db3";
        public static string database = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DBNAME);
        public static ToDoManager _manager;

        public static ToDoManager Manager
        {
            get {
                if (_manager == null)
                    _manager = new ToDoManager(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DBNAME));
                return _manager;
            }
            set { 
                if(value == null)
                    value = new ToDoManager(database);
                _manager = value; }
        }


        public App()
        {
            InitializeComponent();
            Manager = new ToDoManager(database);
            MainPage = new ToDoPage();
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
