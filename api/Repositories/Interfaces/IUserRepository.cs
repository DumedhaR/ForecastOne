using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Models;

namespace api.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id); // can be null
        Task<User?> GetUserProfile(int id);
        Task<User> CreateAsync(User userModel);
        Task<User?> UpdateAsync(int id, UpdateUserDto userDto);
        Task<User?> DeleteAsync(int id);
        Task<User?> AddFavoriteCityAsync(int userId, int cityId);
        Task<User?> DeleteFavoriteCityAsync(int userId, int cityId);
    }
}