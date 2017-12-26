using AutoMapper;
using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViveVolar.Entities;
using ViveVolar.Models;
using ViveVolar.Repositories.FlightRepository;

namespace ViveVolar.Services.FlightService
{
    public class FlightService: IFlightService
    {
        private IFlightRepository _flightRepository;

        public FlightService(IFlightRepository _flightRepository)
        {
            this._flightRepository = _flightRepository;
        }

        public async Task<Flight> AddAsync(Flight entity)
        {
            var flightEntity = Mapper.Map<FlightEntity>(entity);
            return Mapper.Map<Flight>( await this._flightRepository.AddAsync(flightEntity));
        }

        public async Task<Flight> AddOrUpdateAsync(Flight entity)
        {
            var flightEntity = Mapper.Map<FlightEntity>(entity);
            return Mapper.Map<Flight>( await this._flightRepository.AddOrUpdateAsync(flightEntity));
        }

        public async Task<Flight> DeleteAsync(string id)
        {
            var entityToDelete = await this._flightRepository.GetAsync(id);
            return Mapper.Map<Flight>( await this._flightRepository.DeleteAsync(entityToDelete));
        }

        public async Task<IEnumerable<Flight>> GetAllAsync()
        {
            return Mapper.Map<IEnumerable<Flight>>( await this._flightRepository.GetAllAsync());
        }

        public async Task<Flight> GetAsync(string id)
        {
            return Mapper.Map<Flight>( await this._flightRepository.GetAsync(id));
        }

        public async Task<IEnumerable<Flight>> QueryAsync(TableQuery<FlightEntity> query)
        {
            return Mapper.Map<IEnumerable<Flight>>( await this._flightRepository.QueryAsync(query));
        }

        public async Task<Flight> UpdateAsync(Flight entity)
        {
            var flightEntity = Mapper.Map<FlightEntity>(entity);
            return Mapper.Map<Flight>(  await this._flightRepository.UpdateAsync(flightEntity));
        }
    }
}
