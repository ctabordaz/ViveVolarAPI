using AutoMapper;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViveVolar.Entities;
using ViveVolar.Models;
using ViveVolar.Repositories.BookingRepository;
using ViveVolar.Services.FlightService;

namespace ViveVolar.Services.BookingService
{
    public class BookingService:IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IFlightService _flightService;

        public BookingService(IBookingRepository _bookingRepository, IFlightService _flightService)
        {
            this._bookingRepository =_bookingRepository;
            this._flightService = _flightService;
        }


        public async Task<Booking> AddAsync(Booking entity)
        {
            var bookingEntity = Mapper.Map<BookingEntity>(entity);
            return Mapper.Map<Booking>( await this._bookingRepository.AddAsync(bookingEntity));
        }

        public async Task<Booking> AddOrUpdateAsync(Booking entity)
        {
            var bookingEntity = Mapper.Map<BookingEntity>(entity);
            if (string.IsNullOrEmpty(bookingEntity.RowKey))
            {
                bookingEntity.RowKey = (long.MaxValue - DateTime.UtcNow.Ticks).ToString();
            }
            return Mapper.Map<Booking>( await this._bookingRepository.AddOrUpdateAsync(bookingEntity));
        }

        public async Task<Booking> DeleteAsync(string id)
        {
            var entityToDelete = await this._bookingRepository.GetAsync(id);
            return Mapper.Map<Booking>( await this._bookingRepository.DeleteAsync(entityToDelete));
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return Mapper.Map<IEnumerable<Booking>>( await this._bookingRepository.GetAllAsync());
        }

        public async Task<Booking> GetAsync(string id)
        {
            return Mapper.Map<Booking>(await this._bookingRepository.GetAsync(id));
        }

        public async Task<IEnumerable<Booking>> GetByUserIdAsync(string user)
        {
            string query = TableQuery.GenerateFilterCondition("UserId", QueryComparisons.Equal, user);
            var bookings = await QueryAsync(query);
            foreach (var booking in bookings)
            {
                booking.Flight = await this._flightService.GetAsync(booking.FlightId);
            }

            return bookings;
        }

        public async Task<IEnumerable<Booking>> QueryAsync(string query)
        {
            return Mapper.Map<IEnumerable<Booking>>( await this._bookingRepository.QueryAsync(query));
        }

        public async Task<Booking> UpdateAsync(Booking entity)
        {
            var bookingEntity = Mapper.Map<BookingEntity>(entity);
            return Mapper.Map<Booking>( await this._bookingRepository.UpdateAsync(bookingEntity));
        }


    }
}
