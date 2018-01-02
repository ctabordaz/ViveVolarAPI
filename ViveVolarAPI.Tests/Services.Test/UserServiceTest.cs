using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViveVolar.Repositories.UserRepository;
using AutoMapper;
using ViveVolar.WebApi.Mapper;
using ViveVolar.Models;
using ViveVolar.Entities;
using ViveVolar.Services.UserService;
using ViveVolar.Services.FlightService;
using ViveVolar.Services.BookingService;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace ViveVolarAPI.Tests.Services.Test
{
    [TestClass]
    public class UserServiceTest
    {
        Mock<IUserRepository> mockUserRepository;
        Mock<IBookingService> mockBookingRepository;
        Mock<IFlightService> mockFlightRepository;

        [TestInitialize]
        public void Initialize()
        {
            
            Mapper.Initialize(m => m.AddProfile<DefaultProfile>());
            
            mockUserRepository = new Mock<IUserRepository>();
            mockBookingRepository = new Mock<IBookingService>();
            mockFlightRepository = new Mock<IFlightService>();
        }

        [TestMethod]
        public async Task AddAsyncTestAsync()
        {
            //Arrange
            User user = new User() { Name = "Camilo" };
            UserEntity userEntity = new UserEntity() { Name = "Camilo" };
            mockUserRepository.Setup(r => r.AddAsync(It.IsAny<UserEntity>())).ReturnsAsync(userEntity);
            IUserService service = new UserService(mockUserRepository.Object, mockFlightRepository.Object, mockBookingRepository.Object);

            //Act
            var result = await service.AddAsync(user);

            //Assert
            Assert.AreEqual(user.Name, result.Name);

        }

        [TestMethod]
        public async Task AddOrUpdateAsyncTestAsync()
        {
            //Arrange
            User user = new User() { Name = "Camilo" };
            UserEntity userEntity = new UserEntity() { Name = "Camilo" };
            mockUserRepository.Setup(r => r.AddOrUpdateAsync(It.IsAny<UserEntity>())).ReturnsAsync(userEntity);
            IUserService service = new UserService(mockUserRepository.Object, mockFlightRepository.Object, mockBookingRepository.Object);

            //Act
            var result = await service.AddOrUpdateAsync(user);

            //Assert
            Assert.AreEqual(user.Name, result.Name);

        }

        [TestMethod]
        public async Task AuthenticateTestAsync()
        {
            //Arrange
            User user = new User() { Name = "Camilo" };
            byte[] passwordSalt;
            byte[] passwordHash;
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("123"));
            }
            
            UserEntity userEntity = new UserEntity() { Name = "Camilo", PasswordHash = passwordHash, PasswordSalt= passwordSalt };
            mockUserRepository.Setup(r => r.GetAsync(It.IsAny<string>())).ReturnsAsync(userEntity);
            IUserService service = new UserService(mockUserRepository.Object, mockFlightRepository.Object, mockBookingRepository.Object);

            //Act           
            var result = await service.Authenticate("camilo", "123");

            //Assert
            Assert.IsNotNull(result.Token);

        }

        [TestMethod]
        public async Task AuthenticateUserInvalidTestAsync()
        {
            //Arrange
            User user = new User() { Name = "Camilo" };
            byte[] passwordSalt;
            byte[] passwordHash;
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("123"));
            }

           
            mockUserRepository.Setup(r => r.GetAsync(It.IsAny<string>())).ReturnsAsync((UserEntity) null);
            IUserService service = new UserService(mockUserRepository.Object, mockFlightRepository.Object, mockBookingRepository.Object);

            //Act           
            var result = await service.Authenticate("camilo", "123");

            //Assert
            Assert.IsNull(result);

        }

        [TestMethod]
        public async Task AuthenticateInvalidPasswordTestAsync()
        {
            //Arrange
            User user = new User() { Name = "Camilo" };
            byte[] passwordSalt;
            byte[] passwordHash;
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("324"));
            }


            UserEntity userEntity = new UserEntity() { Name = "Camilo", PasswordHash = passwordHash, PasswordSalt = passwordSalt };
            mockUserRepository.Setup(r => r.GetAsync(It.IsAny<string>())).ReturnsAsync(userEntity);
            IUserService service = new UserService(mockUserRepository.Object, mockFlightRepository.Object, mockBookingRepository.Object);

            //Act           
            var result = await service.Authenticate("camilo", "123");

            //Assert
            Assert.IsNull(result);

        }

        [TestMethod]
        public async Task CreateTestAsync()
        {
            //Arrange
            User user = new User() { Name = "Camilo" };          

            UserEntity userEntity = new UserEntity() { Name = "Camilo" };
            mockUserRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<UserEntity>());
            mockUserRepository.Setup(r => r.AddAsync(It.IsAny<UserEntity>())).ReturnsAsync(userEntity);
            IUserService service = new UserService(mockUserRepository.Object, mockFlightRepository.Object, mockBookingRepository.Object);

            //Act           
            var result = await service.Create(user, "123");

            //Assert
            Assert.AreEqual(user.Name,result.Name);

        }

        public async Task CreateUserExistsTestAsync()
        {
            //Arrange
            User user = new User() { Name = "Camilo" };

            UserEntity userEntity = new UserEntity() { Name = "Camilo" };
            mockUserRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<UserEntity>() { userEntity});
            mockUserRepository.Setup(r => r.AddAsync(It.IsAny<UserEntity>())).ReturnsAsync(userEntity);
            IUserService service = new UserService(mockUserRepository.Object, mockFlightRepository.Object, mockBookingRepository.Object);

            try
            {
                //Act           
                var result = await service.Create(user, "123");
                Assert.Fail();
            }
            catch (System.Exception)
            {

                //Assert
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public async Task DeleteTestAsync()
        {
            //Arrange
            User user = new User() { Name = "Camilo" };

            UserEntity userEntity = new UserEntity() { Name = "Camilo" };
            mockUserRepository.Setup(r => r.GetAsync(It.IsAny<string>())).ReturnsAsync(userEntity);
            mockUserRepository.Setup(r => r.DeleteAsync(It.IsAny<UserEntity>())).ReturnsAsync(userEntity);
            IUserService service = new UserService(mockUserRepository.Object, mockFlightRepository.Object, mockBookingRepository.Object);

            //Act           
            var result = await service.DeleteAsync("123");

            //Assert
            Assert.AreEqual(user.Name, result.Name);

        }

        [TestMethod]
        public async Task GetAllAsyncTestAsync()
        {
            //Arrange
            User user = new User() { Name = "Camilo" };

            UserEntity userEntity = new UserEntity() { Name = "Camilo" };
            mockUserRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<UserEntity>() { userEntity});            
            IUserService service = new UserService(mockUserRepository.Object, mockFlightRepository.Object, mockBookingRepository.Object);

            //Act           
            var result = await service.GetAllAsync();

            //Assert
            Assert.AreEqual(1, result.Count());

        }

        [TestMethod]
        public async Task GetAsyncTestAsync()
        {
            //Arrange
            User user = new User() { Name = "Camilo" };

            UserEntity userEntity = new UserEntity() { Name = "Camilo" };
            mockUserRepository.Setup(r => r.GetAsync(It.IsAny<string>())).ReturnsAsync( userEntity );
            IUserService service = new UserService(mockUserRepository.Object, mockFlightRepository.Object, mockBookingRepository.Object);

            //Act           
            var result = await service.GetAsync("123!");

            //Assert
            Assert.AreEqual(user.Name , result.Name);

        }

        [TestMethod]
        public async Task QueryTestAsync()
        {
            //Arrange
            User user = new User() { Name = "Camilo" };

            UserEntity userEntity = new UserEntity() { Name = "Camilo" };
            mockUserRepository.Setup(r => r.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<UserEntity>() { userEntity });
            IUserService service = new UserService(mockUserRepository.Object, mockFlightRepository.Object, mockBookingRepository.Object);

            //Act           
            var result = await service.QueryAsync("");

            //Assert
            Assert.AreEqual(1, result.Count());

        }

        [TestMethod]
        public async Task UpdateTestAsync()
        {
            //Arrange
            User user = new User() { Name = "Camilo" };

            UserEntity userEntity = new UserEntity() { Name = "Camilo" };
            mockUserRepository.Setup(r => r.UpdateAsync(It.IsAny<UserEntity>())).ReturnsAsync(userEntity);
            IUserService service = new UserService(mockUserRepository.Object, mockFlightRepository.Object, mockBookingRepository.Object);

            //Act           
            var result = await service.UpdateAsync(user);

            //Assert
            Assert.AreEqual(user.Name, result.Name);

        }

    }
}
