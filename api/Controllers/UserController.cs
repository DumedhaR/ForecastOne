using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos;
using api.Mappers;
using api.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // private readonly AppDBContext _context; // '_' used as naming convention (prefix) for private fields.
        private readonly IUserRepository _userRepo;
        public UserController(IUserRepository userRepo)
        {
            // _context = context; // no need use 'this' as we use '_' for private fields.
            _userRepo = userRepo;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepo.GetAllAsync();
            var userDtos = users.Select(s => s.ToUserDto());
            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user.ToUserDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequestDto userDto)
        {
            var userModel = userDto.ToUserModel();
            await _userRepo.CreateAsync(userModel);
            return CreatedAtAction(nameof(GetById), new { id = userModel.Id }, userModel.ToUserDto());

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateUserRequestDto userDto)
        {
            var userModel = await _userRepo.UpdateAsync(id, userDto);

            if (userModel == null)
            {
                return NotFound();
            }
            return Ok(userModel.ToUserDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var userModel = await _userRepo.DeleteAsync(id);

            if (userModel == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}