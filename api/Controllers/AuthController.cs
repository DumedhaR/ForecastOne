using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Repositories.Interfaces;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{

    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("google/signin")]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action(nameof(GoogleCallback), "Auth", null, Request.Scheme)
            };

            // Challenge Google authentication
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("google/callback")]
        public async Task<IActionResult> GoogleCallback()
        {
            // Authenticate using middleware

            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

            if (!result.Succeeded)
                return Redirect("http://localhost:5173/signIn?error=google_auth_failed");

            var userCleims = result.Principal;

            var authResult = await _authService.SignInOrUpGoogleUserAsync(userCleims);

            if (authResult == null)
            {
                return BadRequest("http://localhost:5173/signIn?error=google_auth_failed");
            }

            // Redirect frontend with JWT
            return Redirect("http://localhost:5173/weather");
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] CreateUserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // if model or dto validations failed
            }
            var authResult = await _authService.SignUpLocalUserAsync(userDto);

            if (authResult == null)
            {
                return BadRequest("Authentication failed.");
            }
            return Ok(authResult);
        }

        [HttpPost("signout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.SignOutAsync();
            return NoContent();
        }

    }
}