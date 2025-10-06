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
        Task<User> CreateAsync(User userModel);
        Task<User?> UpdateAsync(int id, UpdateUserRequestDto userDto);
        Task<User?> DeleteAsync(int id);
    }
}