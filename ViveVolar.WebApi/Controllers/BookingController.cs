using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ViveVolar.Entities;
using ViveVolar.Models;
using ViveVolar.Services.BookingService;

namespace ViveVolar.WebApi.Controllers
{
    [RoutePrefix("api/Booking")]
    public class BookingController : ApiController
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService _bookingService)
        {
            this._bookingService = _bookingService;
        }

        public async Task<HttpResponseMessage> Get()
        {
            try
            {
                var bookinglist = await this._bookingService.GetAllAsync();
                return Request.CreateResponse(HttpStatusCode.OK, bookinglist);
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        public async Task<HttpResponseMessage> Get(string id)
        {
            try
            {
                var booking = await this._bookingService.GetAsync(id);
                return Request.CreateResponse(HttpStatusCode.OK, booking);
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        public async Task<HttpResponseMessage> Post(Booking newBooking)
        {
            try
            {
                var booking = await this._bookingService.AddOrUpdateAsync(newBooking);
                return Request.CreateResponse(HttpStatusCode.OK, booking);
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        public async Task<HttpResponseMessage> Delete(string id)
        {
            try
            {
                var booking = await this._bookingService.DeleteAsync(id);
                return Request.CreateResponse(HttpStatusCode.OK, booking);
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
