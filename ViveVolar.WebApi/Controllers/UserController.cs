using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ViveVolar.Entities;
using ViveVolar.Models;
using ViveVolar.Services.UserService;
using ViveVolar.WebApi.Filters;

namespace ViveVolar.WebApi.Controllers
{

    [RoutePrefix("api/User")]
    [JwtAuthentication]
    [Authorize]

    public class UserController : ApiController
    {
        private readonly IUserService _userService;
             

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

        [AllowAnonymous]
        public async Task<HttpResponseMessage> Post(User newUser)
        {
            try
            {
                var user = await this._userService.Create(newUser,newUser.Password);
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [AllowAnonymous]
        [Route("auth")]
        [HttpPost()]

        public async Task<HttpResponseMessage> Authenticate(User login)
        {
            try
            {
                User user = await _userService.Authenticate(login.Email, login.Password);

                if (user == null)
                    throw new UnauthorizedAccessException();

                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
            catch(UnauthorizedAccessException uex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, uex);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
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
