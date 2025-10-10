using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Models;

namespace api.Repositories.Interfaces
{
    public interface ICityRepository
    {
        Task<List<City>> GetAllAsync();
        Task<City?> GetByIdAsync(int id);
        Task<City> CreateAsync(City cityModel);
        Task<City?> UpdateAsync(int id, UpdateCityDto cityDto);
        Task<City?> DeleteAsync(int id);
    }
}