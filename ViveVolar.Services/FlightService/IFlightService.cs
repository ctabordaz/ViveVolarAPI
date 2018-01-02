using System.Collections.Generic;
using System.Threading.Tasks;
using ViveVolar.Entities;
using ViveVolar.Models;
using ViveVolar.Services.Base;

namespace ViveVolar.Services.FlightService
{
    public interface IFlightService: IBaseService<Flight>
    {
        Task<IEnumerable<Flight>> GetByUserIdAsync(string userId);
        Task<IEnumerable<Flight>> SearchFlightsAsync(Search search);
    }
}
