using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace ViveVolar.Repositories.Base
{
    public class TableBaseRepository : ITableBaseRepository
    {
        private readonly CloudTableClient _client;
        private readonly string _tableName;
        private IDictionary<string, CloudTable> _tables;

        public TableBaseRepository(string connection)
        {
            CloudStorageAccount account = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting(connection));
            _client = account.CreateCloudTableClient();
            _tables = new Dictionary<string, CloudTable>();
            _tableName = CloudConfigurationManager.GetSetting("StorageTableName");
        }

        public async Task<object> AddAsync(ITableEntity entity)
        {
            var table = await EnsureTable();

            TableOperation insertOperation = TableOperation.Insert(entity);

            TableResult result = await table.ExecuteAsync(insertOperation);

            return result.Result;
        }

        public async Task<object> AddOrUpdateAsync(ITableEntity entity)
        {
            var table = await EnsureTable();

            TableOperation insertOrUpdateOperation = TableOperation.InsertOrReplace(entity);

            TableResult result = await table.ExecuteAsync(insertOrUpdateOperation);

            return result.Result;
        }

        public async Task<object> DeleteAsync(ITableEntity entity)
        {
            var table = await EnsureTable();

            TableOperation deleteOperation = TableOperation.Delete(entity);

            TableResult result = await table.ExecuteAsync(deleteOperation);

            return result.Result;
        }

        public async Task<object> UpdateAsync(ITableEntity entity)
        {
            var table = await EnsureTable();

            TableOperation updateOperation = TableOperation.Replace(entity);

            TableResult result = await table.ExecuteAsync(updateOperation);

            return result.Result;
        }

     

        async Task<IEnumerable<T>> ITableBaseRepository.QueryAsync<T>(TableQuery<T> query)
        {
            var table = await EnsureTable();

            bool shouldConsiderTakeCount = query.TakeCount.HasValue;

            return shouldConsiderTakeCount ? await QueryAsyncWithTakeCount(table, query) : await QueryAsync(table, query); throw new NotImplementedException();
        }

        private async Task<CloudTable> EnsureTable()
        {
            if (!_tables.ContainsKey(_tableName))
            {
                var table = _client.GetTableReference(_tableName);
                await table.CreateIfNotExistsAsync();
                _tables[_tableName] = table;
            }

            return _tables[_tableName];
        }

        public async Task<T> GetAsync<T>(string partitionKey, string rowKey) where T : class, ITableEntity
        {
            var table = await EnsureTable();

            TableOperation retrieveOperation = TableOperation.Retrieve<T>(partitionKey, rowKey);

            TableResult result = await table.ExecuteAsync(retrieveOperation);

            return result.Result as T;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(string partitionKey) where T : class, ITableEntity, new()
        {
            var table = await EnsureTable();

            TableContinuationToken token = null;
            var entities = new List<T>();
            do
            {
                var queryResult = await table.ExecuteQuerySegmentedAsync(new TableQuery<T>().Where(TableQuery.GenerateFilterCondition("PartitionKey",QueryComparisons.Equal,partitionKey)), token);
                entities.AddRange(queryResult.Results);
                token = queryResult.ContinuationToken;
            } while (token != null);

            return entities;
        }

        private async Task<IEnumerable<T>> QueryAsync<T>(CloudTable table, TableQuery<T> query)
            where T : class, ITableEntity, new()
        {
            var entities = new List<T>();

            TableContinuationToken token = null;
            do
            {
                var queryResult = await table.ExecuteQuerySegmentedAsync(query, token);
                entities.AddRange(queryResult.Results);
                token = queryResult.ContinuationToken;
            } while (token != null);

            return entities;
        }

        private async Task<IEnumerable<T>> QueryAsyncWithTakeCount<T>(CloudTable table, TableQuery<T> query)
            where T : class, ITableEntity, new()
        {
            var entities = new List<T>();

            const int maxEntitiesPerQueryLimit = 1000;
            var totalTakeCount = query.TakeCount;
            var remainingRecordsToTake = query.TakeCount;

            TableContinuationToken token = null;
            do
            {
                query.TakeCount = remainingRecordsToTake >= maxEntitiesPerQueryLimit ? maxEntitiesPerQueryLimit : remainingRecordsToTake;
                remainingRecordsToTake -= query.TakeCount;

                var queryResult = await table.ExecuteQuerySegmentedAsync(query, token);
                entities.AddRange(queryResult.Results);
                token = queryResult.ContinuationToken;
            } while (entities.Count < totalTakeCount && token != null);

            return entities;
        }
    }
}
