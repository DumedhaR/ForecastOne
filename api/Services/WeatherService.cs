using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;


namespace api.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private readonly string _apiKey;

        public WeatherService(HttpClient httpClient, IMemoryCache cache, IConfiguration config)
        {
            _httpClient = httpClient;
            _cache = cache;
            _apiKey = config["OPENWEATHER_API_KEY"] ?? throw new Exception("API key missing");
        }

        public async Task<object> GetWeatherByCityAsync(string cityCode)
        {
            if (_cache.TryGetValue(cityCode, out object? cachedData))
            {
                return new { cityCode, weatherData = cachedData, fromCache = true };
            }

            var url = $"https://api.openweathermap.org/data/2.5/weather?id={cityCode}&units=metric&appid={_apiKey}";

            try
            {
                var response = await _httpClient.GetFromJsonAsync<object>(url);

                if (response != null)
                    _cache.Set(cityCode, response, TimeSpan.FromMinutes(5));

                return new { cityCode, weatherData = response, fromCache = false };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching city {cityCode}: {ex.Message}");
                return new { cityCode, error = "Failed to fetch weather data" };
            }
        }

        public async Task<List<object>> GetAllWeatherAsync(IEnumerable<string> cityCodes)
        {
            var tasks = cityCodes.Select(code => GetWeatherByCityAsync(code));
            var results = await Task.WhenAll(tasks);
            return results.ToList();
        }
    }
}