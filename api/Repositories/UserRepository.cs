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

        public async Task<User?> UpdateAsync(int id, UpdateUserRequestDto userDto)
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
    }
}