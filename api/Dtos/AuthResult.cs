using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos
{
    public class AuthResult
    {
        public UserDto User { get; set; } = null!;
        public string Token { get; set; } = string.Empty;
    }
}