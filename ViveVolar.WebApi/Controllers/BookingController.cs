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
using ViveVolar.Services.FlightService;
using ViveVolar.Services.Smpt;
using ViveVolar.WebApi.Filters;

namespace ViveVolar.WebApi.Controllers
{
   
    [RoutePrefix("api/Booking")]
    [JwtAuthentication]
    [Authorize]
    public class BookingController : ApiController
    {
        private readonly IBookingService _bookingService;
        private readonly IFlightService _flightService;
        private readonly ISmptService _smptService;

        public BookingController(IBookingService _bookingService, IFlightService _flightService, ISmptService _smptService)
        {
            this._bookingService = _bookingService;
            this._flightService = _flightService;
            this._smptService = _smptService;
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
                var flight = await this._flightService.GetAsync(booking.FlightId);
                string body = string.Format("Su vuelo {0}-{1} para el {2} ha sido reservado exitosamente",flight.SourceCity,flight.DestinationCity,flight.Date.ToShortDateString());
                string subject = string.Format("Reserva #{0}", booking.Id);
                this._smptService.sendEmail(booking.UserId, subject, body);
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

        [Route("getByUser")]
        public async Task<HttpResponseMessage> getByUser(string id)
        {
            try
            {
                var booking = await this._bookingService.GetByUserIdAsync(id);
                return Request.CreateResponse(HttpStatusCode.OK, booking);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError,ex);
            }
        }
    }
}
