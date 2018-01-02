using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViveVolar.Services.UserService;
using System.Collections.Generic;
using ViveVolar.Models;
using System.Threading.Tasks;
using ViveVolar.WebApi.Controllers;
using System.Net.Http;
using System.Web.Http;
using System.Linq;

namespace ViveVolarAPI.Tests.WebApi.Test
{
    [TestClass]
    public class UserControllerTest
    {
        Mock<IUserService> mockUserService;

        [TestInitialize]
        public void Initialize()
        {
            mockUserService = new Mock<IUserService>();
        }

        [TestMethod]
        public async Task GetAsync()
        {
            try
            {
                //Arrange
                List<User> usersMock = new List<User>();
                usersMock.Add(new User() { Name = "Camilo" });
                mockUserService.Setup(r => r.GetAllAsync()).ReturnsAsync(usersMock);
                UserController controller = new UserController(mockUserService.Object);
                controller.Request = new HttpRequestMessage();
                controller.Configuration = new HttpConfiguration();

                //Act
                var response = await controller.Get();


                //Assert
                IEnumerable<User> users;
                response.TryGetContentValue<IEnumerable<User>>(out users);
                Assert.AreEqual(1, users.Count());


            }
            catch (Exception )
            {
                Assert.Fail();
            }

        }

        [TestMethod]
        public async Task GetExceptionAsync()
        {
            try
            {
                //Arrange
                List<User> usersMock = new List<User>();
                usersMock.Add(new User() { Name = "Camilo" });
                mockUserService.Setup(r => r.GetAllAsync()).ThrowsAsync(new Exception());
                UserController controller = new UserController(mockUserService.Object);
                controller.Request = new HttpRequestMessage();
                controller.Configuration = new HttpConfiguration();

                //Act
                var response = await controller.Get();


                //Assert
                Assert.AreEqual(System.Net.HttpStatusCode.InternalServerError, response.StatusCode);


            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task GetbyEmailAsync()
        {
            try
            {
                //Arrange
                mockUserService.Setup(r => r.GetAsync(It.IsAny<string>())).ReturnsAsync(new User() { Name = "Camilo" });
                UserController controller = new UserController(mockUserService.Object);
                controller.Request = new HttpRequestMessage();
                controller.Configuration = new HttpConfiguration();

                //Act
                var response = await controller.Get("camilo@hotm.co");


                //Assert
                User user;
                response.TryGetContentValue<User>(out user);
                Assert.AreEqual("Camilo",user.Name);


            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }

        [TestMethod]
        public async Task GetbyEmailExceptionAsync()
        {
            try
            {
                //Arrange
                mockUserService.Setup(r => r.GetAsync(It.IsAny<string>())).ThrowsAsync(new Exception());
                UserController controller = new UserController(mockUserService.Object);
                controller.Request = new HttpRequestMessage();
                controller.Configuration = new HttpConfiguration();

                //Act
                var response = await controller.Get("camilo@hotm.co");


                //Assert
                Assert.AreEqual(System.Net.HttpStatusCode.InternalServerError, response.StatusCode);


            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }

        [TestMethod]
        public async Task PostAsync()
        {
            try
            {
                //Arrange
                mockUserService.Setup(r => r.Create(It.IsAny<User>(),It.IsAny<string>())).ReturnsAsync(new User() { Name = "Camilo" });
                UserController controller = new UserController(mockUserService.Object);
                controller.Request = new HttpRequestMessage();
                controller.Configuration = new HttpConfiguration();

                //Act
                var response = await controller.Post(new User() { Password="122"});


                //Assert
                User user;
                response.TryGetContentValue<User>(out user);
                Assert.AreEqual("Camilo", user.Name);


            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }

        [TestMethod]
        public async Task PostExceptionAsync()
        {
            try
            {
                //Arrange
                mockUserService.Setup(r => r.Create(It.IsAny<User>(), It.IsAny<string>())).ThrowsAsync(new Exception());
                UserController controller = new UserController(mockUserService.Object);
                controller.Request = new HttpRequestMessage();
                controller.Configuration = new HttpConfiguration();

                //Act
                var response = await controller.Post(new User() { Password = "122" });


                //Assert
                Assert.AreEqual(System.Net.HttpStatusCode.InternalServerError, response.StatusCode);


            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }

        [TestMethod]
        public async Task AuthenticateAsync()
        {
            try
            {
                //Arrange
                mockUserService.Setup(r => r.Authenticate(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new User() { Name = "Camilo" });
                UserController controller = new UserController(mockUserService.Object);
                controller.Request = new HttpRequestMessage();
                controller.Configuration = new HttpConfiguration();

                //Act
                var response = await controller.Authenticate(new User() { Password = "122" });


                //Assert
                User user;
                response.TryGetContentValue<User>(out user);
                Assert.AreEqual("Camilo", user.Name);


            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }

        [TestMethod]
        public async Task AuthenticateUnAuthorizeAsync()
        {
            try
            {
                //Arrange
                mockUserService.Setup(r => r.Authenticate(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((User)null);
                UserController controller = new UserController(mockUserService.Object);
                controller.Request = new HttpRequestMessage();
                controller.Configuration = new HttpConfiguration();

                //Act
                var response = await controller.Authenticate(new User() { Password = "122" });


                //Assert
                Assert.AreEqual(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);


            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }

        [TestMethod]
        public async Task AuthenticateExceptionAsync()
        {
            try
            {
                //Arrange
                mockUserService.Setup(r => r.Authenticate(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new Exception());
                UserController controller = new UserController(mockUserService.Object);
                controller.Request = new HttpRequestMessage();
                controller.Configuration = new HttpConfiguration();

                //Act
                var response = await controller.Authenticate(new User() { Password = "122" });


                //Assert
                Assert.AreEqual(System.Net.HttpStatusCode.InternalServerError, response.StatusCode);


            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }

        [TestMethod]
        public async Task DeleteAsync()
        {
            try
            {
                //Arrange
                mockUserService.Setup(r => r.DeleteAsync(It.IsAny<string>())).ReturnsAsync(new User() { Name = "Camilo" });
                UserController controller = new UserController(mockUserService.Object);
                controller.Request = new HttpRequestMessage();
                controller.Configuration = new HttpConfiguration();

                //Act
                var response = await controller.Delete("camilo");


                //Assert
                User user;
                response.TryGetContentValue<User>(out user);
                Assert.AreEqual("Camilo", user.Name);


            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }


        [TestMethod]
        public async Task DeleteExceptionAsync()
        {
            try
            {
                //Arrange
                mockUserService.Setup(r => r.DeleteAsync(It.IsAny<string>())).ThrowsAsync(new Exception());
                UserController controller = new UserController(mockUserService.Object);
                controller.Request = new HttpRequestMessage();
                controller.Configuration = new HttpConfiguration();

                //Act
                var response = await controller.Delete("camilo");


                //Assert
                Assert.AreEqual(System.Net.HttpStatusCode.InternalServerError, response.StatusCode);

            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }
    }
}
