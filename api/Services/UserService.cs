using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using api.Dtos;
using api.Models;
using api.Repositories.Interfaces;
using api.Services.Interfaces;

namespace api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;

        }
        public async Task<List<User>> GetAllAsync()
        {
            var userModels = await _userRepository.GetAllAsync();
            return userModels;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            var userModel = await _userRepository.GetByIdAsync(id);
            if (userModel == null)
            {
                return null;
            }
            return userModel;
        }

        public async Task<User?> UpdateByIdAsync(int id, UpdateUserDto userDto)
        {
            var updatedUserModel = await _userRepository.UpdateAsync(id, userDto);
            if (updatedUserModel == null)
            {
                return null;
            }
            return updatedUserModel;
        }

        public async Task<User?> DeleteByIdAsync(int id)
        {
            var deletedUserModel = await _userRepository.DeleteAsync(id);
            if (deletedUserModel == null)
            {
                return null;
            }
            return deletedUserModel;
        }

        public async Task<User?> AddFavoriteCityAsync(int userId, int cityId)
        {
            var userModel = await _userRepository.AddFavoriteCityAsync(userId, cityId);
            if (userModel == null)
            {
                return null;
            }
            return userModel;
        }

        public async Task<User?> DeleteFavoriteCityAsync(int userId, int cityId)
        {
            var userModel = await _userRepository.DeleteFavoriteCityAsync(userId, cityId);

            if (userModel == null)
            {
                return null;
            }

            return userModel;
        }
    }
}