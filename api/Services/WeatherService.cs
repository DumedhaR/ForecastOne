using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Services.Interfaces;
using api.Settings;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;


namespace api.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly OpenWeatherMapSettings _settings;
        private readonly IMemoryCache _cache;
        private readonly ICityService _cityService;

        public WeatherService(
            HttpClient httpClient,
            IOptions<OpenWeatherMapSettings> options,
            IMemoryCache cache,
            ICityService cityService)
        {
            _httpClient = httpClient;
            _settings = options.Value;
            _cache = cache;
            _cityService = cityService;
        }

        public async Task<WeatherResult?> GetWeatherByCityId(int id)
        {
            var cacheKey = $"weather_{id}";
            if (_cache.TryGetValue(cacheKey, out object? cachedData))
            {
                return new WeatherResult { CityId = id, Source = "Cache", Data = cachedData };
            }
            var url = $"weather?id={id}&units=metric&appid={_settings.ApiKey}";
            try
            {
                var response = await _httpClient.GetFromJsonAsync<object>(url);
                if (response != null)
                {
                    _cache.Set(cacheKey, response, TimeSpan.FromMinutes(5));
                    return new WeatherResult { CityId = id, Source = "API", Data = response };
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<WeatherResult>?> GetAllWeather()
        {
            var cities = _cityService.GetAllCityCodes();

            var tasks = cities.Select(id => GetWeatherByCityId(id)).ToList();

            var results = await Task.WhenAll(tasks);

            var success = results.Where(r => r != null).ToList();

            if (success.Count == 0)
            {
                return null;
            }

            return success!;
        }
    }
}