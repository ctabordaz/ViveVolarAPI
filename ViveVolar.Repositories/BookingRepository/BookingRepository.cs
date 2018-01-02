using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViveVolar.Entities;
using ViveVolar.Repositories.Base;

namespace ViveVolar.Repositories.BookingRepository
{
    public class BookingRepository: IBookingRepository
    {
        private ITableBaseRepository _tableRepository;
        private const string _partitionKey = "Booking";

        public BookingRepository()
        {
            this._tableRepository = new TableBaseRepository("StorageConnectionString");
        }

        public async Task<BookingEntity> AddAsync(BookingEntity entity)
        {
            return (BookingEntity)await this._tableRepository.AddAsync(entity);
        }

        public async Task<BookingEntity> AddOrUpdateAsync(BookingEntity entity)
        {
            return (BookingEntity)await this._tableRepository.AddOrUpdateAsync(entity);
        }

        public async Task<BookingEntity> DeleteAsync(BookingEntity entity)
        {
            return (BookingEntity)await this._tableRepository.DeleteAsync(entity);
        }

        public async Task<IEnumerable<BookingEntity>> GetAllAsync()
        {
            return await this._tableRepository.GetAllAsync<BookingEntity>(_partitionKey);
        }

        public async Task<BookingEntity> GetAsync(string rowKey)
        {
            return (BookingEntity)await this._tableRepository.GetAsync<BookingEntity>(_partitionKey, rowKey);
        }

        public async Task<IEnumerable<BookingEntity>> QueryAsync(string squery)
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
            TableQuery<BookingEntity> query = new TableQuery<BookingEntity>()
                .Where(combinedQuery);
            return await this._tableRepository.QueryAsync<BookingEntity>(query);
        }

        public async Task<BookingEntity> UpdateAsync(BookingEntity entity)
        {
            return (BookingEntity)await this._tableRepository.UpdateAsync(entity);
        }
    }
}
