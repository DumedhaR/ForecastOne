using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("AuthProviders")]
    public class AuthProvider
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // "local", "google", "github"
        public List<UserLogin> UserLogins { get; set; } = new List<UserLogin>();
    }
}