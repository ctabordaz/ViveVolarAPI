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

namespace ViveVolar.Services.BookingService
{
    public class BookingService:IBookingService
    {
        private IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository _bookingRepository)
        {
            this._bookingRepository =_bookingRepository;
        }


        public async Task<Booking> AddAsync(Booking entity)
        {
            var bookingEntity = Mapper.Map<BookingEntity>(entity);
            return Mapper.Map<Booking>( await this._bookingRepository.AddAsync(bookingEntity));
        }

        public async Task<Booking> AddOrUpdateAsync(Booking entity)
        {
            var bookingEntity = Mapper.Map<BookingEntity>(entity);
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
            return await QueryAsync(query);
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
