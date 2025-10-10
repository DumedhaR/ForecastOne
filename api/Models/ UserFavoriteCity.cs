using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{

    [Table("UserFavoriteCities")]
    public class UserFavoriteCity
    {
        public int UserId { get; set; }
        public int CityId { get; set; }

        // Navigation
        public User User { get; set; } = null!;
        public City City { get; set; } = null!;
    }
}