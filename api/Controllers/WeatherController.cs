using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/weather")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherService _weatherService;

        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }


        [HttpGet("{cityId}")]
        public async Task<IActionResult> GetWeatherByCity(string cityId)
        {
            var result = await _weatherService.GetWeatherByCityAsync(cityId);

            if (result.GetType().GetProperty("error") != null)
                return StatusCode(500, result);

            return Ok(result);
        }

        // [HttpGet]
        // public async Task<IActionResult> GetAllWeather([FromQuery] int page = 1, [FromQuery] int limit = 10)
        // {
        //     var allCityCodes = "";

        //     var weatherData = await _weatherService.GetAllWeatherAsync(allCityCodes);

        //     // Pagination
        //     var paged = weatherData.Skip((page - 1) * limit).Take(limit).ToList();

        //     return Ok(new
        //     {
        //         status = "success",
        //         results = paged.Count,
        //         data = paged
        //     });
        // }

    }
}