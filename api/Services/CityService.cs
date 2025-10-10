using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using api.Models;
using api.Services.Interfaces;

namespace api.Services
{
    public class CityService : ICityService

    {
        private readonly List<City> _cities;
        public CityService()
        {
            string dataPath = Path.Combine(AppContext.BaseDirectory, "Data", "cities.json");
            try
            {
                string json = File.ReadAllText(dataPath);
                var dict = JsonSerializer.Deserialize<Dictionary<string, List<City>>>(json);
                _cities = dict?["List"] ?? new List<City>();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error reading city data: " + e.Message);
                Environment.Exit(1); // fail fast as it critical req
            }
        }
        public List<City> GetAll()
        {
            return _cities;
        }

        public List<int> GetAllCityCodes()
        {
            var cityCodes = _cities.Select(c => c.Id).ToList();
            return cityCodes;
        }

        public City? GetById(int id)
        {
            var city = _cities.FirstOrDefault(c => c.Id == id);
            if (city == null)
            {
                return null;
            }
            return city;
        }
    }
}