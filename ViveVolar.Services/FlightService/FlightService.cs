using AutoMapper;
using Microsoft.WindowsAzure.Storage.Table;
using System;
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

        public async Task<IEnumerable<Flight>> GetByUserIdAsync(string userId) 
        {
            string query = TableQuery.GenerateFilterCondition("UserId", QueryComparisons.Equal, userId);
            return await QueryAsync(query);
        }

        public async Task<IEnumerable<Flight>> QueryAsync(string query)
        {
            return Mapper.Map<IEnumerable<Flight>>( await this._flightRepository.QueryAsync(query));
        }

        public async Task<IEnumerable<Flight>> SearchFlightsAsync(Search search)
        {
            string query = string.Empty;
            bool requireAnd = false;

            if(search.Chairs != null)
            {
                query = TableQuery.GenerateFilterConditionForInt("Chairs", QueryComparisons.Equal, search.Chairs.Value);
                requireAnd = true;
            }
            

            if (search.InitDate != null)
            {
                if (requireAnd)
                {
                    query = query + " and ";
                }                
                query += dateQuery(search.InitDate.Value, QueryComparisons.GreaterThanOrEqual);
                requireAnd = true;

            }

            if (search.EndDate != null)
            {
                if (requireAnd)
                {
                    query = query + " and ";
                }

                query += dateQuery(search.EndDate.Value, QueryComparisons.LessThanOrEqual);
                requireAnd = true;
            }

            if (!string.IsNullOrEmpty(search.SourceCity))
            {
                if (requireAnd)
                {
                    query = query + " and ";
                }
                query += TableQuery.GenerateFilterCondition("SourceCity", QueryComparisons.Equal, search.SourceCity);
                requireAnd = true;
            }


            if (!string.IsNullOrEmpty(search.DestinationCity))
            {
                if (requireAnd)
                {
                    query = query + " and ";
                }
                query += TableQuery.GenerateFilterCondition("DestinationCity", QueryComparisons.Equal, search.DestinationCity);
                
            }

            return await QueryAsync(query);
        }

        public async Task<Flight> UpdateAsync(Flight entity)
        {
            var flightEntity = Mapper.Map<FlightEntity>(entity);
            return Mapper.Map<Flight>(  await this._flightRepository.UpdateAsync(flightEntity));
        }

        private string dateQuery(DateTime date, string opertator)
        {
            string query = string.Empty;

            query = string.Format("Date {0} datetime'{1}'", opertator ,date.ToString("yyyy-MM-dd"));

            return query;
        }
    }
}
