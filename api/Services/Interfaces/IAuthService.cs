using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Dtos;
using api.Models;

namespace api.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<User?> SignUpLocalUserAsync(CreateUserDto userDto);
        public Task<User?> SignInOrUpGoogleUserAsync(ClaimsPrincipal claims);
        public Task<User?> SignInLocalUserAsync(AuthRequestDto credentials);
        public Task SignOutAsync();

    }
}