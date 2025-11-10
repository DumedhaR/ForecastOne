using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Mappers;
using api.Models;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{

    // [Authorize(Policy = "AdminOrUser")]
    [Route("api/city")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCities()
        {
            var cities = await _cityService.GetAllAsync();
            var cityDtos = cities.Select(c => c.ToCityDto());
            return Ok(cityDtos);
        }

        // GET: api/city/codes
        [HttpGet("codes")]
        public async Task<ActionResult> GetAllCityCodes()
        {
            var codes = await _cityService.GetAllCityCodesAsync();
            return Ok(codes);
        }

        // GET: api/city/{code}
        [HttpGet("{code:int}")]
        public async Task<ActionResult> GetCityByCode([FromRoute] int code)
        {
            var city = await _cityService.GetByIdAsync(code);
            if (city == null)
            {
                return NotFound($"City with code {code} not found.");
            }

            return Ok(city.ToCityDto());
        }

    }
}