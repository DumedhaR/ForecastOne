using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos;
using api.Mappers;
using api.Models;
using api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDBContext _context;
        public UserRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> CreateAsync(User userModel)
        {
            await _context.Users.AddAsync(userModel);
            await _context.SaveChangesAsync();
            return userModel;
        }

        public async Task<User?> DeleteAsync(int id)
        {
            var userModel = await _context.Users.FirstOrDefaultAsync(s => s.Id == id);
            if (userModel == null)
            {
                return null;
            }
            _context.Users.Remove(userModel);
            await _context.SaveChangesAsync();
            return userModel;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> UpdateAsync(int id, UpdateUserDto userDto)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(s => s.Id == id);
            if (existingUser == null)
            {
                return null;
            }
            existingUser.UpdateUserModel(userDto);
            await _context.SaveChangesAsync();
            return existingUser;
        }

        public async Task<User?> GetUserProfile(int id)
        {
            var userProfile = await _context.Users
                .Include(u => u.FavoriteCities)
                .ThenInclude(ufc => ufc.City)
                .FirstOrDefaultAsync(u => u.Id == id);
            if (userProfile == null)
            {
                return null;
            }

            return userProfile;
        }

        public async Task<User?> AddFavoriteCityAsync(int userId, int cityId)
        {
            var isUserExist = await _context.Users.AnyAsync(u => u.Id == userId);
            if (!isUserExist)
            {
                return null;
            }
            var isCityExist = await _context.Cities.AnyAsync(c => c.Id == cityId);
            if (!isCityExist)
            {
                return null;
            }
            var isFavExist = await _context.UserFavoriteCities
                .AnyAsync(ufc => ufc.UserId == userId && ufc.CityId == cityId);

            if (isFavExist)
            {
                return await GetUserProfile(userId);
            }

            await _context.UserFavoriteCities.AddAsync(new UserFavoriteCity
            {
                UserId = userId,
                CityId = cityId
            });
            await _context.SaveChangesAsync();

            return await GetUserProfile(userId);
        }

        public async Task<User?> DeleteFavoriteCityAsync(int userId, int cityId)
        {
            var existingfavCity = await _context.UserFavoriteCities
                .FirstOrDefaultAsync(fc => fc.UserId == userId && fc.CityId == cityId);

            if (existingfavCity == null)
                return null;

            _context.UserFavoriteCities.Remove(existingfavCity);
            await _context.SaveChangesAsync();

            return await GetUserProfile(userId);
        }

    }
}