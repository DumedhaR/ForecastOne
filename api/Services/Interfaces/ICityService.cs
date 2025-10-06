using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Services.Interfaces
{
    public interface ICityService
    {
        List<City> GetAllCities();

        List<string> GetAllCityCodes();

        City? GetCityByCode(string code);
    }
}