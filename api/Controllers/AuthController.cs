using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Repositories.Interfaces;
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
                return Unauthorized("Google authentication failed");

            var user = result.Principal;

            // Extract claims
            var subId = user.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var email = user.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var firstName = user.FindFirst(System.Security.Claims.ClaimTypes.GivenName)?.Value;
            var lastName = user.FindFirst(System.Security.Claims.ClaimTypes.Surname)?.Value;

            Console.WriteLine($"SubId: {subId}");
            Console.WriteLine($"Email: {email}");
            Console.WriteLine($"First Name: {firstName}");
            Console.WriteLine($"Last Name: {lastName}");

            // Redirect frontend with JWT
            return Redirect("http://localhost:5173/weather");
        }


    }
}