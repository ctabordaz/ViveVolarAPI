using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViveVolar.Entities;
using ViveVolar.Repositories.FlightRepository;

namespace ViveVolar.Services.FlightService
{
    public class FlightService: IFlightService
    {
        private IFlightRepository _flightRepository;

        public FlightService()
        {
            this._flightRepository = new FlightRepository();
        }


        public async Task<FlightEntity> AddAsync(FlightEntity entity)
        {
            return await this._flightRepository.AddAsync(entity);
        }

        public async Task<FlightEntity> AddOrUpdateAsync(FlightEntity entity)
        {
            return await this._flightRepository.AddOrUpdateAsync(entity);
        }

        public async Task<FlightEntity> DeleteAsync(string id)
        {
            var entityToDelete = await this._flightRepository.GetAsync(id);
            return await this._flightRepository.DeleteAsync(entityToDelete);
        }

        public async Task<IEnumerable<FlightEntity>> GetAllAsync()
        {
            return await this._flightRepository.GetAllAsync();
        }

        public async Task<FlightEntity> GetAsync(string id)
        {
            return await this._flightRepository.GetAsync(id);
        }

        public async Task<IEnumerable<FlightEntity>> QueryAsync(TableQuery<FlightEntity> query)
        {
            return await this._flightRepository.QueryAsync(query);
        }

        public async Task<FlightEntity> UpdateAsync(FlightEntity entity)
        {
            return await this._flightRepository.UpdateAsync(entity);
        }
    }
}
