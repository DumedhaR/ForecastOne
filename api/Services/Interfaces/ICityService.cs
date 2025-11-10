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
        Task<List<City>> GetAll();
        Task<List<int>> GetAllCityCodes();
        Task<City?> GetById(int id);
        Task<City?> UpdateById(int id, UpdateCityDto cityDto);
        Task<City?> DeleteById(int id);
    }
}