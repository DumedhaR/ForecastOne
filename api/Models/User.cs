namespace api.Models
{
    public class User
    {
        public int Id { get; set; }
        public int SubId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Picture { get; set; } = string.Empty;
        public List<int> FavoriteCityIds { get; set; } = new();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
