using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Users")]
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Picture { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigations
        public List<UserLogin> UserLogins { get; set; } = new List<UserLogin>();
        public List<UserFavoriteCity> FavoriteCities { get; set; } = new List<UserFavoriteCity>();

    }
}