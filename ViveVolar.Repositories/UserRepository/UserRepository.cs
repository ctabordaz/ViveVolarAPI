﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
using ViveVolar.Entities;
using ViveVolar.Repositories.Base;

namespace ViveVolar.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private ITableBaseRepository _tableRepository;
        private const string _partitionKey = "User";

        public UserRepository()
        {
            this._tableRepository = new TableBaseRepository("StorageConnectionString");
        }

        public async Task<UserEntity> AddAsync(UserEntity entity)
        {
            return (UserEntity) await this._tableRepository.AddAsync( entity);
        }

        public async Task<UserEntity> AddOrUpdateAsync(UserEntity entity)
        {
            return (UserEntity)await this._tableRepository.AddOrUpdateAsync( entity);
        }

        public async Task<UserEntity> DeleteAsync(UserEntity entity)
        {
            return (UserEntity)await this._tableRepository.DeleteAsync(entity);
        }

        public async Task<IEnumerable<UserEntity>> GetAllAsync()
        {
            return await this._tableRepository.GetAllAsync<UserEntity>(_partitionKey);
        }

        public async Task<UserEntity> GetAsync(string rowKey)
        {
            return (UserEntity) await this._tableRepository.GetAsync<UserEntity>(_partitionKey, rowKey);
        }

        public async Task<IEnumerable<UserEntity>> QueryAsync(string squery)
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
            TableQuery<UserEntity> query = new TableQuery<UserEntity>()
                .Where(combinedQuery);
            return await this._tableRepository.QueryAsync<UserEntity>(query);
        }

        public async Task<UserEntity> UpdateAsync(UserEntity entity)
        {
            return (UserEntity)await this._tableRepository.UpdateAsync(entity);
        }
    }

}
