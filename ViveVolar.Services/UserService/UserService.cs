using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.WindowsAzure.Storage.Table;
using ViveVolar.Entities;
using ViveVolar.Models;
using ViveVolar.Repositories.UserRepository;
using ViveVolar.Services.BookingService;
using ViveVolar.Services.FlightService;

namespace ViveVolar.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IFlightService _flightService;
        private readonly IBookingService _bookingServie;

        public UserService(IUserRepository _userRepository, IFlightService _flightService, IBookingService _bookingServie)
        {
            this._userRepository = _userRepository;
            this._flightService = _flightService;
            this._bookingServie = _bookingServie;
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
            var user = Mapper.Map<User>( await this._userRepository.GetAsync(id));
            user.Flights = await this._flightService.GetByUserIdAsync(id);
            user.Bookings = await this._bookingServie.GetByUserIdAsync(id);
            return user;
        }
       
        public async Task<IEnumerable<User>> QueryAsync(string query)
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
