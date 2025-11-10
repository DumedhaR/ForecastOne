using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using api.Dtos;
using api.Mappers;
using api.Models;
using api.Repositories;
using api.Repositories.Interfaces;
using api.Services.Interfaces;

namespace api.Services
{
    public class CityService : ICityService

    {
        private readonly ICityRepository _cityRepository;
        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<List<City>> GetAllAsync()
        {
            var cityModels = await _cityRepository.GetAllAsync();
            return cityModels;
        }

        public async Task<List<int>> GetAllCityCodesAsync()
        {
            var cityCodes = await _cityRepository.GetAllIdsAsync();
            return cityCodes;
        }

        public async Task<City?> GetByIdAsync(int id)
        {
            var cityModel = await _cityRepository.GetByIdAsync(id);

            if (cityModel == null)
            {
                return null;
            }

            return cityModel;
        }

        public async Task<City?> UpdateByIdAsync(int id, UpdateCityDto cityDto)
        {
            var updatedCityModel = await _cityRepository.UpdateAsync(id, cityDto);
            if (updatedCityModel == null)
            {
                return null;
            }
            return updatedCityModel;
        }

        public async Task<City?> DeleteByIdAsync(int id)
        {
            var deletedCityModel = await _cityRepository.DeleteAsync(id);
            if (deletedCityModel == null)
            {
                return null;
            }
            return deletedCityModel;
        }

        public async Task<City?> CreateCityAsync(City city)
        {
            var existingCityModel = await _cityRepository.GetByIdAsync(city.Id);

            if (existingCityModel != null)
            {
                return null;
            }

            var cityModel = await _cityRepository.CreateAsync(city);

            return cityModel;
        }
    }
}