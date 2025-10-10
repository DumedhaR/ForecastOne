using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Services.Interfaces
{
    public interface ICityService
    {
        List<City> GetAll();

        List<int> GetAllCityCodes();

        City? GetById(int id);
    }
}