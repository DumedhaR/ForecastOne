using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api.Dtos;
using api.Mappers;
using api.Models;
using api.Repositories.Interfaces;
using api.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace api.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IUserRepository _userRepository;

        private readonly IConfiguration _config;
        public AuthService(IAuthRepository authRepository, IUserRepository userRepository, IConfiguration config)
        {
            _authRepository = authRepository;
            _userRepository = userRepository;
            _config = config;
        }

        public string GenerateJwtToken(User user, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, role)

            };

            var jwtKey = _config["Jwt:Key"];
            var jwtIssuer = _config["Jwt:Issuer"];
            var jwtAudience = _config["Jwt:Audience"];
            var expiryDays = _config["Jwt:ExpiryDays"];

            if (string.IsNullOrEmpty(jwtKey))
                throw new ArgumentNullException(nameof(jwtKey), "Jwt key not configured.");
            if (string.IsNullOrEmpty(jwtIssuer))
                throw new ArgumentNullException(nameof(jwtIssuer), "jwt issuer not configured.");
            if (string.IsNullOrEmpty(jwtAudience))
                throw new ArgumentNullException(nameof(jwtAudience), "jwt audience not configured.");
            if (string.IsNullOrEmpty(expiryDays))
                throw new ArgumentNullException(nameof(expiryDays), "jwt expiry days not configured.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(int.Parse(expiryDays)),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<AuthResult?> SignUpLocalUserAsync(CreateUserDto userDto)
        {
            var isUserExist = await _authRepository.GetByEmailAsync(userDto.Email);
            if (isUserExist != null)
            {
                return null;
            }
            var newUser = userDto.ToUserModel();
            var defaultRole = await _authRepository.GetRoleByNameAsync("User");
            if (defaultRole == null)
            {
                throw new Exception("Default role not found. Please seed roles first.");
            }
            newUser.RoleId = defaultRole.Id;
            var userModel = await _userRepository.CreateAsync(newUser);
            await _authRepository.CreateLocalUserAsync(userModel.Id, userDto.Password, userDto.Email);
            var token = GenerateJwtToken(userModel, defaultRole.Name);
            return new AuthResult
            {
                User = userModel.ToUserDto(),
                Token = token
            };
        }
        public Task<AuthResult?> SignUpGoogleUserAsync(ClaimsPrincipal claims)
        {
            throw new NotImplementedException();
        }

    }
}