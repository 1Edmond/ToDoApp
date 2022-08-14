using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using ToDoApp.Model;
using Xamarin.Forms;

using System.Threading.Tasks;
using ToDoApp.Extensions;
using ToDoApp.Data;
using Xamarin.Essentials;
using System.IO;

namespace ToDoApp.ViewModel
{
    public class ToDoViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<ToDo> ToDos { get; private set; }

        public ToDoManager manager = App.Manager;
        public ToDoViewModel()
        {          
            manager = new ToDoManager(App.database);
            var dos = manager.GetAll();
            if(dos != null)
            {
                dos.Sort((x, y) => { return x.AddDate.Date.CompareTo(y.AddDate.Date); });
                ToDos = new ObservableCollection<ToDo>(dos);
            }
        }

        public ICommand DeleteCommand => new Command(async (obj) => 
        {
            var todo = obj as ToDo;
            ToDos.Remove(todo);

            var result = await manager.Delete(todo);
            if(result)
            {
                var dos = manager.GetAll();
                if (dos != null)
                {
                    dos.Sort((x, y) => { return x.AddDate.Date.CompareTo(y.AddDate.Date); });
                    ToDos = new ObservableCollection<ToDo>(dos);
                    OnPropertyChanged("ToDos");
                }
                InputValidationColor = Color.FromHex("#4ba3c3");
                InputValidation = $"Great, the ToDo {todo.Description} has been remove successfully";
                OnPropertyChanged("InputValidationColor");
                OnPropertyChanged("InputValidation");
            }
        });

        public ICommand RefreshCommand => new Command( () =>
        {
            var dos = manager.GetAll();
            if (dos != null)
               ToDos = new ObservableCollection<ToDo>(dos);
        });

        public string InputValidation { get; set; }
        public Color InputValidationColor { get; set; }

        public bool IsOk = true;
       
        public string NewToDoInput { get; set; } = String.Empty;

        public ICommand AddCommand => new Command(async () =>
        {
            InputValidationColor = Color.FromHex("#4ba3c3 ");
            InputValidation = "Great, your ToDo has been add successfully";
            IsOk = true;
            if (NewToDoInput == null || String.IsNullOrEmpty(NewToDoInput))
            {
                IsOk = false;
                InputValidationColor = Color.Red;
                InputValidation = "Error you can't add empty ToDo";
            }

            if (IsOk)
            {
                var todo = new ToDo() { Description = NewToDoInput.PremiereLettreMajiscule(), Completed = false,AddDate = DateTime.Now };
                if(!manager.Exist(todo.Description,todo.Completed))
                {
                    await manager.Add(todo);
                    ToDos.Add(todo);
                    OnPropertyChanged("ToDos");   
                }
                else
                {
                    InputValidationColor = Color.Red;
                    InputValidation = $"Error the todo {NewToDoInput.PremiereLettreMajiscule()} already exist";
                }
                
            }
            NewToDoInput = "";
            OnPropertyChanged("NewToDoInput");
            OnPropertyChanged("InputValidationColor");
            OnPropertyChanged("InputValidation");            
        });

        
        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
         
        }

    }
}
