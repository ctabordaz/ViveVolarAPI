using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ViveVolar.Entities;
using ViveVolar.Models;
using ViveVolar.Services.FlightService;

namespace ViveVolar.WebApi.Controllers
{
    [RoutePrefix("api/Flight")]
    public class FlightController: ApiController
    {
        private readonly IFlightService _flightService;

        public FlightController(IFlightService _flightService)
        {
            this._flightService = _flightService;
        }

        public async Task<HttpResponseMessage> Get()
        {
            try
            {
                var flightlist = await this._flightService.GetAllAsync();
                return Request.CreateResponse(HttpStatusCode.OK, flightlist);
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
                var flight = await this._flightService.GetAsync(id);
                return Request.CreateResponse(HttpStatusCode.OK, flight);
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        public async Task<HttpResponseMessage> Post(Flight newFlight)
        {
            try
            {
                var flight = await this._flightService.AddOrUpdateAsync(newFlight);
                return Request.CreateResponse(HttpStatusCode.OK, flight);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        public async Task<HttpResponseMessage> Delete(string id)
        {
            try
            {
                var flight = await this._flightService.DeleteAsync(id);
                return Request.CreateResponse(HttpStatusCode.OK, flight);
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

    }
}