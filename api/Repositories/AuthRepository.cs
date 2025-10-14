using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDBContext _context;

        public AuthRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<UserLogin> CreateUserLoginAsync(UserLogin userLogin)
        {
            await _context.UserLogins.AddAsync(userLogin);
            await _context.SaveChangesAsync();
            return userLogin;
        }

        public async Task<UserLogin?> DeleteAsync(int userId)
        {
            var userLogin = await _context.UserLogins.FirstOrDefaultAsync(ul => ul.UserId == userId);
            if (userLogin == null)
            {
                return null;
            }
            _context.UserLogins.Remove(userLogin);
            await _context.SaveChangesAsync();
            return userLogin;
        }

        public async Task<UserLogin?> GetByEmailAsync(string email)
        {
            var userLogin = await _context.UserLogins.FirstOrDefaultAsync(ul => ul.Email == email);

            return userLogin;
        }

        public async Task<UserLogin?> GetByUserIdAsync(int userId)
        {
            var userLogin = await _context.UserLogins.FirstOrDefaultAsync(ul => ul.UserId == userId);

            return userLogin;
        }

        public async Task<UserLogin?> GetByProviderAsync(int providerId, string subId)
        {
            var userLogin = await _context.UserLogins.FirstOrDefaultAsync(ul =>
                ul.ProviderId == providerId &&
                ul.SubId == subId
            );

            return userLogin;
        }

        public async Task<UserLogin?> UpdateUserPasswordAsync(int userId, string password)
        {
            var existingUserLogin = await _context.UserLogins.FirstOrDefaultAsync(ul => ul.UserId == userId && !ul.IsExternal);
            if (existingUserLogin == null)
            {
                return null;
            }
            if (!string.IsNullOrWhiteSpace(password))
                existingUserLogin.Password = password;

            await _context.SaveChangesAsync();
            return existingUserLogin;
        }

        public async Task<List<UserRole>> GetAllRolesAsync()
        {
            return await _context.UserRoles.ToListAsync();
        }

        public async Task<UserRole?> GetRoleByIdAsync(int id)
        {
            return await _context.UserRoles.FindAsync(id);
        }

        public async Task<UserRole?> GetRoleByNameAsync(string name)
        {
            return await _context.UserRoles.FirstOrDefaultAsync(ur => ur.Name == name);
        }

        public async Task<AuthProvider?> GetProviderByNameAsync(string name)
        {
            return await _context.AuthProviders.FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task<List<AuthProvider>?> GetAllProvidersAsync()
        {
            return await _context.AuthProviders.ToListAsync();
        }
    }
}