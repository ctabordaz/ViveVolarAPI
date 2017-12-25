using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ViveVolar.Services.Base
{
    public interface IBaseService<T> where T : TableEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> QueryAsync(TableQuery<T> query);
        Task<T> GetAsync(string id);
        Task<T> AddOrUpdateAsync(T entity);
        Task<T> DeleteAsync(string id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
    }
}
