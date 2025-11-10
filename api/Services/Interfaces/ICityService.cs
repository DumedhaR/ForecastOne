using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Models;

namespace api.Services.Interfaces
{
    public interface ICityService
    {
        public Task<City?> CreateCityAsync(City city);
        public Task<List<City>> GetAllAsync();
        public Task<List<int>> GetAllCityCodesAsync();
        public Task<City?> GetByIdAsync(int id);
        public Task<City?> UpdateByIdAsync(int id, UpdateCityDto cityDto);
        public Task<City?> DeleteByIdAsync(int id);
    }
}