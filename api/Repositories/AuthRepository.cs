using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Repositories.Interfaces;

namespace api.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        public Task<UserLogin> CreateExternalUserAsync(int userId, int providerId, string subId, string? email = null)
        {
            throw new NotImplementedException();
        }

        public Task<UserLogin> CreateLocalUserAsync(int userId, string password, string Email)
        {
            throw new NotImplementedException();
        }

        public Task<UserLogin> DeleteAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<UserLogin> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<UserLogin> GetByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<UserLogin> GetByProviderAsync(int providerId, string subId)
        {
            throw new NotImplementedException();
        }

        public Task<UserLogin> GetByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<UserLogin> UpdateLocalUserAsync(int userId, string password)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserRole>> GetAllRolesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserRole> GetRoleByNameAsync(string role)
        {
            throw new NotImplementedException();
        }

    }
}