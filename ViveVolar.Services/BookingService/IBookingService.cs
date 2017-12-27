using System.Collections.Generic;
using System.Threading.Tasks;
using ViveVolar.Entities;
using ViveVolar.Models;
using ViveVolar.Services.Base;

namespace ViveVolar.Services.BookingService
{
    public interface IBookingService: IBaseService<Booking>
    {
        Task<IEnumerable<Booking>> GetByUserIdAsync(string user);
    }
}
