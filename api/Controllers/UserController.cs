using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDBContext _context; // '_' used as naming convention (prefix) for private fields.
        public UserController(AppDBContext context)
        {
            _context = context; // no need use 'this' as we use '_' for private fields.

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _context.Users.ToList().Select(s => s.ToUserDto());
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user.ToUserDto());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateUserRequestDto userDto)
        {
            var userModel = userDto.ToUserModel();
            _context.Users.Add(userModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = userModel.Id }, userModel.ToUserDto());

        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateUserRequestDto userDto)
        {
            var userModel = _context.Users.FirstOrDefault(s => s.Id == id);

            if (userModel == null)
            {
                return NotFound();
            }

            userModel.UpdateUserModel(userDto);
            _context.SaveChanges();
            return Ok(userModel.ToUserDto());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var userModel = _context.Users.FirstOrDefault(s => s.Id == id);

            if (userModel == null)
            {
                return NotFound();
            }

            _context.Users.Remove(userModel);
            _context.SaveChanges();
            return NoContent();
        }
    }
}