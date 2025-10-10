using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Models;
using api.Repositories.Interfaces;

namespace api.Repositories
{
    public class CityRepository : ICityRepository
    {
        public Task<City> CreateAsync(City cityModel)
        {
            throw new NotImplementedException();
        }

        public Task<City?> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<City>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<City?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<City?> UpdateAsync(int id, UpdateCityDto cityDto)
        {
            throw new NotImplementedException();
        }
    }
}