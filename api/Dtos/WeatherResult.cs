using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos
{
    public class WeatherResult
    {
        public int CityId { get; set; }
        public string Source { get; set; } = string.Empty;
        public object? Data { get; set; }
    }

}