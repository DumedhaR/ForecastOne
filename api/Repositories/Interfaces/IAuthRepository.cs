using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<UserLogin> CreateUserLoginAsync(UserLogin userLogin);
        Task<UserLogin?> UpdateUserPasswordAsync(int userId, string password);
        Task<UserLogin?> GetByUserIdAsync(int userId);
        Task<UserLogin?> GetByEmailAsync(string email);
        Task<UserLogin?> GetByProviderAsync(int providerId, string subId);
        Task<UserLogin?> DeleteAsync(int userId);
        Task<List<UserRole>> GetAllRolesAsync();
        Task<UserRole?> GetRoleByIdAsync(int id);
        Task<UserRole?> GetRoleByNameAsync(string name);
        Task<List<AuthProvider>?> GetAllProvidersAsync();
        Task<AuthProvider?> GetProviderByNameAsync(string name);
    }
}