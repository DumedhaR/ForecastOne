using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;

namespace api.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherResult?> GetWeatherByCityId(int id);
        Task<List<WeatherResult>?> GetAllWeather();
    }
}