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
            var defaultRole = await _authRepository.GetRoleByNameAsync("user") ?? throw new Exception("Default role not found. Please seed roles first.");
            newUser.RoleId = defaultRole.Id;
            var userModel = await _userRepository.CreateAsync(newUser);
            UserLogin userLogin = new()
            {
                UserId = userModel.Id,
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                Email = userDto.Email,
                IsExternal = false
            };
            await _authRepository.CreateUserLoginAsync(userLogin);
            var token = GenerateJwtToken(userModel, defaultRole.Name);
            return new AuthResult
            {
                User = userModel.ToUserDto(),
                Token = token
            };
        }

        public async Task<AuthResult?> SignInGoogleUserAsync(UserLogin userLogin)
        {
            var existingUser = await _userRepository.GetByIdAsync(userLogin.UserId);

            if (existingUser == null)
            {
                return null;
            }
            var userRole = await _authRepository.GetRoleByIdAsync(existingUser.RoleId) ?? throw new Exception("Role not found for given Id.");

            var token = GenerateJwtToken(existingUser, userRole.Name);

            return new AuthResult
            {
                User = existingUser.ToUserDto(),
                Token = token
            };
        }

        public async Task<AuthResult?> SignInOrUpGoogleUserAsync(ClaimsPrincipal userClaims)
        {
            // Extract claims
            var subId = userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (subId == null)
            {
                return null;
            }
            var defaultProvider = await _authRepository.GetProviderByNameAsync("google") ?? throw new Exception($"Default Provider not found. Please seed provider first.");
            var defaultRole = await _authRepository.GetRoleByNameAsync("user") ?? throw new Exception("Default role not found. Please seed roles first.");
            var existingUserLogin = await _authRepository.GetByProviderAsync(defaultProvider.Id, subId);
            if (existingUserLogin != null)
            {
                return await SignInGoogleUserAsync(existingUserLogin);
            }
            var email = (userClaims.FindFirst(ClaimTypes.Email)?.Value) ?? throw new Exception("Email not found in claims.");
            var firstName = userClaims.FindFirst(ClaimTypes.GivenName)?.Value;
            var lastName = userClaims.FindFirst(ClaimTypes.Surname)?.Value;
            User newUser = new()
            {
                FirstName = firstName ?? "Unknown",
                LastName = lastName ?? "User",
                Email = email,
                RoleId = defaultRole.Id,
            };
            var userModel = await _userRepository.CreateAsync(newUser);
            UserLogin userLogin = new()
            {
                UserId = userModel.Id,
                ProviderId = defaultProvider.Id,
                SubId = subId,
                Email = email,
                IsExternal = true
            };
            await _authRepository.CreateUserLoginAsync(userLogin);
            var token = GenerateJwtToken(userModel, defaultRole.Name);
            return new AuthResult
            {
                User = userModel.ToUserDto(),
                Token = token
            };
        }

        public async Task<AuthResult?> SignInLocalUserAsync(AuthRequestDto credentials)
        {
            var userLogin = await _authRepository.GetByEmailAsync(credentials.Email);

            if (userLogin == null || !BCrypt.Net.BCrypt.Verify(credentials.Password, userLogin.Password))
            {
                return null;
            }

            var userModel = await _userRepository.GetByIdAsync(userLogin.UserId);

            if (userModel == null)
            {
                return null;
            }
            var userRole = await _authRepository.GetRoleByIdAsync(userModel.RoleId) ?? throw new Exception("Role not found for given Id.");

            var token = GenerateJwtToken(userModel, userRole.Name);

            return new AuthResult
            {
                User = userModel.ToUserDto(),
                Token = token
            };
        }
    }
}