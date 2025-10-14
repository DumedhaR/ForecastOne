using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("UserLogins")]
    public class UserLogin
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? ProviderId { get; set; }  // Only for external 
        public bool IsExternal { get; set; }  // "local" or "external"
        public string? Email { get; set; }
        public string? Password { get; set; }  // Only for local
        public string? SubId { get; set; }  // Only for external (OpenID 'sub')
        public DateTime? LastLogin { get; set; }

        // Navigations
        public User User { get; set; } = null!;
        public AuthProvider? Provider { get; set; }
    }
}