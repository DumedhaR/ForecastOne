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
        private readonly IUserRepository _userRepo;
        public UserController(IUserRepository userRepo)
        {
            // _context = context; // no need use 'this' as we use '_' for private fields.
            _userRepo = userRepo;

        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepo.GetAllAsync();
            var userDtos = users.Select(u => u.ToUserDto());
            return Ok(userDtos);
        }

        [Authorize(Policy = "AdminOrUser")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var user = await _userRepo.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user.ToUserDto());
        }

        // [HttpPost]
        // public async Task<IActionResult> Create([FromBody] CreateUserDto userDto)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);  // if model or dto validations failed
        //     }

        //     var userModel = userDto.ToUserModel();
        //     await _userRepo.CreateAsync(userModel);
        //     return CreatedAtAction(nameof(GetById), new { id = userModel.Id }, userModel.ToUserDto());

        // }

        [Authorize(Policy = "AdminOrUser")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateUserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userModel = await _userRepo.UpdateAsync(id, userDto);

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
            var userModel = await _userRepo.DeleteAsync(id);

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
            var userId = 1;
            var userModel = await _userRepo.AddFavoriteCityAsync(userId, cityId);

            if (userModel == null)
            {
                return NotFound();
            }
            return Ok(userModel);

        }

        [Authorize(Policy = "AdminOrUser")]
        [HttpDelete("fav/cities")]
        public async Task<IActionResult> DeleteFavoriteCity([FromBody] int cityId)
        {
            var userId = 1;
            var userModel = await _userRepo.DeleteFavoriteCityAsync(userId, cityId);

            if (userModel == null)
            {
                return NotFound();
            }
            return Ok(userModel);

        }
    }
}