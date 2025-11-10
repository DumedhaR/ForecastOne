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

        public async Task<List<City>> GetAll()
        {
            var cityModels = await _cityRepository.GetAllAsync();
            return cityModels;
        }

        public async Task<List<int>> GetAllCityCodes()
        {
            var cityCodes = await _cityRepository.GetAllIdsAsync();
            return cityCodes;
        }

        public async Task<City?> GetById(int id)
        {
            var cityModel = await _cityRepository.GetByIdAsync(id);

            if (cityModel == null)
            {
                return null;
            }

            return cityModel;
        }

        public async Task<City?> UpdateById(int id, UpdateCityDto cityDto)
        {
            var UpdatedCityModel = await _cityRepository.UpdateAsync(id, cityDto);
            if (UpdatedCityModel == null)
            {
                return null;
            }
            return UpdatedCityModel;
        }

        public async Task<City?> DeleteById(int id)
        {
            var DeletedCityModel = await _cityRepository.DeleteAsync(id);
            if (DeletedCityModel == null)
            {
                return null;
            }
            return DeletedCityModel;
        }

    }
}