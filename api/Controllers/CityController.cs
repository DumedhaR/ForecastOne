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
            var cities = _cityService.GetAll();
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
        [HttpGet("{id}")]
        public ActionResult<City> GetCityByCode(int id)
        {
            var city = _cityService.GetById(id);
            if (city == null)
                return NotFound($"City with code {id} not found.");

            return Ok(city);
        }

    }
}