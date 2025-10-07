using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Models;

namespace api.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToUserDto(this User userModel)
        {
            return new UserDto
            {
                Id = userModel.Id,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                Picture = userModel.Picture,
                FavoriteCityIds = userModel.FavoriteCityIds,
            };
        }

        public static User ToUserModel(this CreateUserRequestDto userDto)
        {
            return new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Picture = userDto.Picture,
                FavoriteCityIds = userDto.FavoriteCityIds,

            };
        }

        public static void UpdateUserModel(this User userModel, UpdateUserRequestDto userDto)
        {
            if (userDto.FirstName != null)
                userModel.FirstName = userDto.FirstName;

            if (userDto.LastName != null)
                userModel.LastName = userDto.LastName;

            if (userDto.Picture != null)
                userModel.Picture = userDto.Picture;
        }
    }
}