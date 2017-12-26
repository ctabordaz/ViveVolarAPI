using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.WindowsAzure.Storage.Table;
using ViveVolar.Entities;
using ViveVolar.Models;
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

        public async Task<User> AddAsync(User entity)
        {
            var userEntity = Mapper.Map<UserEntity>(entity);
            return Mapper.Map<User>(await this._userRepository.AddAsync(userEntity));
        }

        public async Task<User> AddOrUpdateAsync(User entity)
        {
            var userEntity = Mapper.Map<UserEntity>(entity);
            return Mapper.Map<User>(await this._userRepository.AddOrUpdateAsync(userEntity));
        }

        public async Task<User> DeleteAsync(string id)
        {
            var entityToDelete = await this._userRepository.GetAsync(id);
            return Mapper.Map<User>(await this._userRepository.DeleteAsync(entityToDelete));
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return Mapper.Map<IEnumerable<User>>(await this._userRepository.GetAllAsync());
        }

        public async Task<User> GetAsync(string id)
        {
            return Mapper.Map<User>( await this._userRepository.GetAsync(id));
        }
       
        public async Task<IEnumerable<User>> QueryAsync(TableQuery<UserEntity> query)
        {
            return Mapper.Map <IEnumerable<User>>( await this._userRepository.QueryAsync(query));
        }

        public async Task<User> UpdateAsync(User entity)
        {
            var userEntity = Mapper.Map<UserEntity>(entity);
            return Mapper.Map<User>( await this._userRepository.UpdateAsync(userEntity));
        }

     
    }
}
