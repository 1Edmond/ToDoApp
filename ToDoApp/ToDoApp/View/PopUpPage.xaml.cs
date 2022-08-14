using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Model;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUpPage : Popup
    {
        static ToDo todo = new ToDo();
        static string result = "";
        public PopUpPage()
        {
            InitializeComponent();
            
        }
        public PopUpPage(int id)
        {
            InitializeComponent();
            todo = App.Manager.Get(id);
            TodoDate.Text = $"{todo.AddDate.ToString().Substring(0,10)}" +
                $" à {todo.AddDate.ToString().Substring(10)}";
            ToDoDescription.Text = todo.Description;
            ToDoCheckbox.IsChecked = todo.Completed;
            if(todo.Completed)
            TodoCompletionDate.Text = $"{todo.CompletedDate.ToString().Substring(0,10)}" +
                $" à {todo.CompletedDate.ToString().Substring(10)}";
            Completion.Text = todo.Completed ? "Yes" : "No";
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
           
            Dismiss("");
        }

        
    }
}