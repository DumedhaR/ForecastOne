using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Dtos;

namespace api.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<AuthResult?> SignUpLocalUserAsync(CreateUserDto userDto);
        public Task<AuthResult?> SignUpGoogleUserAsync(ClaimsPrincipal claims);

    }
}