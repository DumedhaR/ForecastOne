using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Models;

namespace api.Mappers
{
    public static class CityMapper
    {

        public static CityDto ToCityDto(this City cityModel)
        {
            return new CityDto
            {
                Id = cityModel.Id,
                Country = cityModel.Country,
                Name = cityModel.Name,
            };
        }

        public static City ToCityModel(this CreateCityDto cityDto)
        {
            return new City
            {
                Id = cityDto.Id,
                Name = cityDto.Name,
                Country = cityDto.Country,
            };
        }

    }
}