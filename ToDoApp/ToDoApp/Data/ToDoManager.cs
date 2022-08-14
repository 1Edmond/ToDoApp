using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Extensions;
using ToDoApp.Model;
using Xamarin.Forms;

namespace ToDoApp.Data
{
    public class ToDoManager 
    {

       public static SQLiteAsyncConnection Connection { get; set; }

        public ToDoManager(string database)
        {
            
            Connection = new SQLiteAsyncConnection(database);
            Connection.CreateTableAsync<ToDo>();
        }

        public  async Task<bool> Add(ToDo entity)
        {

            if (Connection == null)
                return false;
            if (entity != null && entity.Id == 0)
                await Connection.InsertAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> Delete(ToDo entity)
        {
            if(Connection == null)
                return false;
            if (entity != null)
               await Connection.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public ToDo Get(int entity)
        {
            if (Connection == null)
                return null;
            return  Connection.GetAsync<ToDo>(entity).Result; 
        }

        public async Task<bool> Update(ToDo entity)
        {
            if (Connection == null)
                return false;
            entity.Completed = !entity.Completed;
            var result =  await Connection.UpdateAsync(entity);
            return result > 0 ?  await Task.FromResult(true) : await Task.FromResult(false);
        }
        public async Task<bool> UpdateAll(List<ToDo> entity)
        {
            if (Connection == null)
                return false;
            var result =  await Connection.UpdateAllAsync(entity);
            return result > 0 ?  await Task.FromResult(true) : await Task.FromResult(false);
        }

        public bool Exist(string description, bool completede)
        {
            var test = Connection.Table<ToDo>().Where(t => t.Description == description && t.Completed == completede).FirstOrDefaultAsync().Result;
            if (test == null)
                return false;
            return true; 
        }

        public List<ToDo> GetAll()
        {
            if(Connection == null)
                return new List<ToDo>();
            var list =  Connection.Table<ToDo>().ToListAsync();
           return list.Result;
        }
    }
}
