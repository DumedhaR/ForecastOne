using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<UserLogin> CreateLocalUserAsync(int userId, string password, string Email);
        Task<UserLogin> CreateExternalUserAsync(int userId, int providerId, string subId, string? email);
        Task<UserLogin> UpdateLocalUserAsync(int userId, string password);
        Task<UserLogin> GetByUserIdAsync(int userId);
        Task<UserLogin> DeleteAsync(int userId);
    }
}