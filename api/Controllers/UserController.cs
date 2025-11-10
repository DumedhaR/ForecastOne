using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos;
using api.Mappers;
using api.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userService;
        public UserController(IUserRepository userService)
        {
            _userService = userService; // no need use 'this' as we use '_' for private fields.

        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            var userDtos = users.Select(u => u.ToUserDto());
            return Ok(userDtos);
        }

        [Authorize(Policy = "AdminOrUser")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user.ToUserDto());
        }

        [Authorize(Policy = "AdminOrUser")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateUserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userModel = await _userService.UpdateAsync(id, userDto);

            if (userModel == null)
            {
                return NotFound();
            }

            return Ok(userModel.ToUserDto());
        }

        [Authorize(Policy = "AdminOrUser")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var userModel = await _userService.DeleteAsync(id);

            if (userModel == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [Authorize(Policy = "AdminOrUser")]
        [HttpPost("fav/cities")]
        public async Task<IActionResult> AddFavoriteCity([FromBody] int cityId)
        {
            var userId = 1; //temp
            var userModel = await _userService.AddFavoriteCityAsync(userId, cityId);

            if (userModel == null)
            {
                return NotFound();
            }
            return Ok(userModel.ToUserDto());

        }

        [Authorize(Policy = "AdminOrUser")]
        [HttpDelete("fav/cities")]
        public async Task<IActionResult> DeleteFavoriteCity([FromBody] int cityId)
        {
            var userId = 1; //temp
            var userModel = await _userService.DeleteFavoriteCityAsync(userId, cityId);

            if (userModel == null)
            {
                return NotFound();
            }
            return Ok(userModel.ToUserDto());

        }
    }
}