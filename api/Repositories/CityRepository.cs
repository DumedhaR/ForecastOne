using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos;
using api.Models;
using api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly AppDBContext _context;
        public CityRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<City> CreateAsync(City cityModel)
        {
            await _context.Cities.AddAsync(cityModel);
            await _context.SaveChangesAsync();
            return cityModel;
        }

        public async Task<List<City>> GetAllAsync()
        {
            return await _context.Cities.ToListAsync();
        }

        public async Task<List<int>> GetAllIdsAsync()
        {
            return await _context.Cities.Select(c => c.Id).ToListAsync();
        }

        public async Task<City?> GetByIdAsync(int id)
        {
            return await _context.Cities.FindAsync(id);
        }

        public async Task<City?> UpdateAsync(int id, UpdateCityDto cityDto)
        {
            var cityModel = await _context.Cities.FindAsync(id);

            if (cityModel == null)
            {
                return null;
            }
            if (cityDto.Name != null)
                cityModel.Name = cityDto.Name;
            if (cityDto.Country != null)
                cityModel.Country = cityDto.Country;

            await _context.SaveChangesAsync();

            return cityModel;
        }

        public async Task<City?> DeleteAsync(int id)
        {
            var cityModel = await _context.Cities.FindAsync(id);

            if (cityModel == null)
            {
                return null;
            }

            _context.Cities.Remove(cityModel);
            await _context.SaveChangesAsync();

            return cityModel;
        }

    }
}