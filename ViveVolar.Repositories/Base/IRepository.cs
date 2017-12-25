using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViveVolar.Repositories.Base
{
    public interface IRepository<T> where T: ITableEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> QueryAsync(TableQuery<T> query);
        Task<T> GetAsync(string rowKey) ;
        Task<T> AddOrUpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
    }
}
