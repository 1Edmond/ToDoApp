using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ToDoApp.Data;
using ToDoApp.Model;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToDoPage : ContentPage
    {

        
        public ToDoPage()
        {
            InitializeComponent();

      
           
        }

        public async void CompleteEvent(object sender, CheckedChangedEventArgs e)
        {
            var temp = new ToDo();
            if(sender is CheckBox checkBox)
            {
                var todo = checkBox.BindingContext as ToDo;
                if(todo != null)
                {
                    temp = todo;
                    temp.Completed = !temp.Completed;
                    temp.CompletedDate = DateTime.Now;
                    var result = await manager.Update(temp);
                    
                }
            }
            
        }

        readonly ToDoManager manager = App._manager;

        private async Task MyToDoList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                 var item = MyToDoList.SelectedItem as ToDo;
                 MyToDoList.SelectedItem = null;
                 await Navigation.ShowPopupAsync(new PopUpPage(item.Id));
            }
            catch (Exception)
            {
                
            }
        }

        private async void MyToDoList_ItemSelected_1(object sender, SelectedItemChangedEventArgs e)
        {

          await  MyToDoList_ItemSelected(sender, e);
        }
    }
}