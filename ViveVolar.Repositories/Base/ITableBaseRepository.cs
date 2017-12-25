using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViveVolar.Entities;

namespace ViveVolar.Repositories.Base
{
    public interface ITableBaseRepository
    {
        Task<IEnumerable<T>> GetAllAsync<T>(string partitionKey) where T : class, ITableEntity, new();
        Task<IEnumerable<T>> QueryAsync<T>(TableQuery<T> query) where T : class, ITableEntity, new();
        Task<T> GetAsync<T>( string partitionKey, string rowKey) where T : class, ITableEntity;
        Task<object> AddOrUpdateAsync(ITableEntity entity);
        Task<object> DeleteAsync(ITableEntity entity);
        Task<object> AddAsync(ITableEntity entity);       
        Task<object> UpdateAsync(ITableEntity entity);
    }
}
