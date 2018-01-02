using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViveVolar.Entities;
using ViveVolar.Repositories.Base;

namespace ViveVolar.Repositories.FlightRepository
{
    public class FlightRepository:IFlightRepository
    {
        private ITableBaseRepository _tableRepository;
        private const string _partitionKey = "Flight";

        public FlightRepository()
        {
            this._tableRepository = new TableBaseRepository("StorageConnectionString");
        }

        public async Task<FlightEntity> AddAsync(FlightEntity entity)
        {
            return (FlightEntity)await this._tableRepository.AddAsync(entity);
        }

        public async Task<FlightEntity> AddOrUpdateAsync(FlightEntity entity)
        {
            return (FlightEntity)await this._tableRepository.AddOrUpdateAsync(entity);
        }

        public async Task<FlightEntity> DeleteAsync(FlightEntity entity)
        {
            return (FlightEntity)await this._tableRepository.DeleteAsync(entity);
        }

        public async Task<IEnumerable<FlightEntity>> GetAllAsync()
        {
            return await this._tableRepository.GetAllAsync<FlightEntity>(_partitionKey);
        }

        public async Task<FlightEntity> GetAsync(string rowKey)
        {
            return (FlightEntity)await this._tableRepository.GetAsync<FlightEntity>(_partitionKey, rowKey);
        }

        public async Task<IEnumerable<FlightEntity>> QueryAsync(string squery)
        {
            string combinedQuery = string.Empty;
            if (string.IsNullOrEmpty(squery))
            {
                combinedQuery = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, _partitionKey);
            }
            else
            {
                combinedQuery = TableQuery.CombineFilters(
                        TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, _partitionKey),
                        TableOperators.And,
                        squery
                );
            }
            TableQuery<FlightEntity> query = new TableQuery<FlightEntity>()
                .Where(combinedQuery);
            return await this._tableRepository.QueryAsync<FlightEntity>(query);
        }

        public async Task<FlightEntity> UpdateAsync(FlightEntity entity)
        {
            return (FlightEntity)await this._tableRepository.UpdateAsync(entity);
        }
    }
}
