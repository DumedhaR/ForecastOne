using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Models;

namespace api.Services.Interfaces
{
    public interface IUserService
    {
        public Task<List<User>> GetAllAsync();
        public Task<User?> GetByIdAsync(int id);
        public Task<User?> UpdateByIdAsync(int id, UpdateUserDto userDto);
        public Task<User?> DeleteByIdAsync(int id);
        public Task<User?> AddFavoriteCityAsync(int userId, int cityId);
        public Task<User?> DeleteFavoriteCityAsync(int userId, int cityId);
    }
}