using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos
{
    public class CityDto
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string state { get; set; } = string.Empty;
        public string country { get; set; } = string.Empty;
        public CoordDto coord { get; set; } = new();
    }

    public class CoordDto
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }
}