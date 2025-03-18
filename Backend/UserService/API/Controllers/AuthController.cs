using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Eventify.Backend.UserService.Application.Interfaces;
using Eventify.Backend.UserService.Infrastructure.Entities;
using Eventify.Backend.UserService.API.DTOs;

namespace Eventify.Backend.UserService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public AuthController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        // Sign Up: Register a new user

        [HttpPost("signup")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
            // Create the user object
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = model.UserName,
                Email = model.Email,
                Role = Role.User, // Assign default role
                PasswordHash =model.Password 
            };

            await _userService.AddUserAsync(user);
            return Ok("User created successfully.");
        }


        // Sign In: Login to obtain JWT token
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] LoginRequest model)
        {
            var user= await _userService.ValidateSignInAsync(model.UserName, model.Password);

            if (user == null)
                return Unauthorized("Invalid username or password.");

            var token = _jwtService.GenerateToken(user);

            return Ok(new { token });
        }
    }



}
