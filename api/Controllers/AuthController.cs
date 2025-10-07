using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("google/signin")]
        public IActionResult GoogleLogin(string returnUrl = "/api/auth/google/callback")
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = returnUrl
            };

            // Challenge Google authentication
            return Challenge(properties, "Google");
        }

        [HttpGet("google/callback")]
        public async Task<IActionResult> GoogleCallback()
        {
            var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!authenticateResult.Succeeded)
            {
                return Redirect("http://localhost:5173/signIn");
            }
            var claims = authenticateResult.Principal.Claims;
            Console.WriteLine(claims);

            // Redirect to frontend dashboard
            return Redirect("http://localhost:5173/weather");
        }

    }
}