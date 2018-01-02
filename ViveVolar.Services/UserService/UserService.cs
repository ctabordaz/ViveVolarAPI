using System;
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
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

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

        public async Task<User> Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;


            var userEntity = await this._userRepository.GetAsync(username);


            if (userEntity == null)
                return null;

            if (!VerifyPasswordHash(password, userEntity.PasswordHash, userEntity.PasswordSalt))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["Secret"].ToString());
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            var user = Mapper.Map<User>(userEntity);
            user.Token = tokenString;
            return user;


        }

        public async Task<User> Create(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Contraseña requerida");

            var users = await this.GetAllAsync();     
            
            if (users.Any(x=> x.Email == user.Email))
                throw new Exception("El correo " + user.Email + " ya existe");

            var userEntity = Mapper.Map<UserEntity>(user);

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            userEntity.PasswordHash = passwordHash;
            userEntity.PasswordSalt = passwordSalt;

            return  Mapper.Map<User>( await this._userRepository.AddAsync(userEntity));

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

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }


    }
}
