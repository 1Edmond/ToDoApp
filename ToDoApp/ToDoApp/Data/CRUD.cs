using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Data
{
    public interface CRUD<T>
    {
        Task<bool> Add(T entity);
        Task<bool> Delete(T entity);
        Task<bool> Update(T entity);
        Task<T> Get(int entity);
        Task<List<T>> GetAll();


    }
}
