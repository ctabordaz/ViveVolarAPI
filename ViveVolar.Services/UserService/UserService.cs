using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
using ViveVolar.Entities;
using ViveVolar.Repositories.UserRepository;

namespace ViveVolar.Services.UserService
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository _userRepository)
        {
            this._userRepository = _userRepository;
        }


        public async Task<UserEntity> AddAsync(UserEntity entity)
        {
            return await this._userRepository.AddAsync(entity);
        }

        public async Task<UserEntity> AddOrUpdateAsync(UserEntity entity)
        {
            return await this._userRepository.AddOrUpdateAsync(entity);
        }

        public async Task<UserEntity> DeleteAsync(string id)
        {
            var entityToDelete = await this._userRepository.GetAsync(id);
            return await this._userRepository.DeleteAsync(entityToDelete);
        }

        public async Task<IEnumerable<UserEntity>> GetAllAsync()
        {
            return await this._userRepository.GetAllAsync();
        }

        public async Task<UserEntity> GetAsync(string id)
        {
            return await this._userRepository.GetAsync(id);
        }

        public async Task<IEnumerable<UserEntity>> QueryAsync(TableQuery<UserEntity> query)
        {
            return await this._userRepository.QueryAsync(query);
        }

        public async Task<UserEntity> UpdateAsync(UserEntity entity)
        {
            return await this._userRepository.UpdateAsync(entity);
        }
    }
}
