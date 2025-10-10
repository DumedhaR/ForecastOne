using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/weather")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _weatherService.GetAllWeather();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{cityId:int}")]
        public async Task<IActionResult> GetByCityId([FromRoute] int cityId)
        {
            var result = await _weatherService.GetWeatherByCityId(cityId);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

    }
}