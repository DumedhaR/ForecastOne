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
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                Picture = userModel.Picture,
                FavoriteCityIds = userModel.FavoriteCities
                                    .Select(ufc => ufc.CityId)
                                    .ToList(),

            };
        }

        public static User ToUserModel(this CreateUserDto userDto)
        {
            return new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Picture = userDto.Picture,

            };
        }


    }
}