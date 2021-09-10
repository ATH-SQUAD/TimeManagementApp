using TimeManagementApp.Database.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimeManagementApp.Database.Repos
{
    public interface IRepository<T>
        where T : class, IEntity
    {
        Task<List<T>> GetAll();
        Task<T> Get(Guid id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(Guid id);
    }
}
