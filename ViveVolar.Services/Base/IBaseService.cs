using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ViveVolar.Services.Base
{
    public interface IBaseService<T,TE> 
        where T : class 
        where TE: TableEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> QueryAsync(TableQuery<TE> query);
        Task<T> GetAsync(string id);
        Task<T> AddOrUpdateAsync(T entity);
        Task<T> DeleteAsync(string id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
    }
}
