using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViveVolar.Entities;
using ViveVolar.Repositories.BookingRepository;

namespace ViveVolar.Services.BookingService
{
    public class BookingService:IBookingService
    {
        private IBookingRepository _bookingRepository;

        public BookingService()
        {
            this._bookingRepository = new BookingRepository();
        }


        public async Task<BookingEntity> AddAsync(BookingEntity entity)
        {
            return await this._bookingRepository.AddAsync(entity);
        }

        public async Task<BookingEntity> AddOrUpdateAsync(BookingEntity entity)
        {
            return await this._bookingRepository.AddOrUpdateAsync(entity);
        }

        public async Task<BookingEntity> DeleteAsync(string id)
        {
            var entityToDelete = await this._bookingRepository.GetAsync(id);
            return await this._bookingRepository.DeleteAsync(entityToDelete);
        }

        public async Task<IEnumerable<BookingEntity>> GetAllAsync()
        {
            return await this._bookingRepository.GetAllAsync();
        }

        public async Task<BookingEntity> GetAsync(string id)
        {
            return await this._bookingRepository.GetAsync(id);
        }

        public async Task<IEnumerable<BookingEntity>> QueryAsync(TableQuery<BookingEntity> query)
        {
            return await this._bookingRepository.QueryAsync(query);
        }

        public async Task<BookingEntity> UpdateAsync(BookingEntity entity)
        {
            return await this._bookingRepository.UpdateAsync(entity);
        }


    }
}
