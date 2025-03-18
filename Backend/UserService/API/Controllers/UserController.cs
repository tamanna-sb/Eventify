using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Eventify.Backend.UserService.Application.Interfaces;
using Eventify.Backend.UserService.Infrastructure.Entities;
using Eventify.Backend.UserService.Common.Exceptions;

namespace Eventify.Backend.UserService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // Get user by Id
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserByIdAsync(Guid userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            return Ok(user);
        }

        // Get all users
        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        /*// Create a new user
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] User user)
        {
            var result = await _userService.AddUserAsync(user);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return CreatedAtAction(nameof(GetUserByIdAsync), new { userId = user.Id }, user);
        }*/

        // Update an existing user
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUserAsync(Guid userId, [FromBody] User user)
        {
            await _userService.UpdateUserAsync(user);
            return Ok(user);
        }

        // Partially update user fields
        [HttpPatch("{userId}")]
        public async Task<IActionResult> PatchUserAsync(Guid userId, [FromBody] Dictionary<string, object> updates)
        {
            await _userService.PatchUserAsync(userId, updates);
            return NoContent();
        }

        // Delete user by Id
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserAsync(Guid userId)
        {
            await _userService.DeleteUserAsync(userId);
            return NoContent();
        }
    }
}
