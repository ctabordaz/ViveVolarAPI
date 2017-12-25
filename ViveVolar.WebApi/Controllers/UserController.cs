using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ViveVolar.Entities;
using ViveVolar.Services.UserService;

namespace ViveVolar.WebApi.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private IUserService _userService;

        public UserController()
        {

        }

        public UserController(IUserService _userService)
        {
            this._userService = _userService;
        }

        public async Task<HttpResponseMessage> Get()
        {
            try
            {
                var userList = await this._userService.GetAllAsync();
                return Request.CreateResponse(HttpStatusCode.OK, userList);
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }       

        public async Task<HttpResponseMessage> Get(string email)
        {
            try
            {
                var user = await this._userService.GetAsync(email);
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        public async Task<HttpResponseMessage> Post(UserEntity newUser)
        {
            try
            {
                var user = await this._userService.AddOrUpdateAsync(newUser);
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        public async Task<HttpResponseMessage> Delete(string email)
        {
            try
            {
                var user = await this._userService.DeleteAsync(email);
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
