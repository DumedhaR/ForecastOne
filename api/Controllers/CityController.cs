using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{

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
        public ActionResult<List<City>> GetAllCities()
        {
            var cities = _cityService.GetAllCities();
            return Ok(cities);
        }

        // GET: api/city/codes
        [HttpGet("codes")]
        public ActionResult<List<string>> GetAllCityCodes()
        {
            var codes = _cityService.GetAllCityCodes();
            return Ok(codes);
        }

        // GET: api/city/{code}
        [HttpGet("{code}")]
        public ActionResult<City> GetCityByCode(string code)
        {
            var city = _cityService.GetCityByCode(code);
            if (city == null)
                return NotFound($"City with code {code} not found.");

            return Ok(city);
        }

    }
}