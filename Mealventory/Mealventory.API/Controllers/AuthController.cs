// Owner 1: "Juan Pablo Ordonez Gomez" has added 74% of the code in this file
// Owner 2: "Daniel Bajenov" has added 26% of the code in this file

using Mealventory.Core.Interfaces;
using Mealventory.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Mealventory.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            request.Email = request.Email.Trim();
            request.Username = request.Username.Trim();

            var existingUser = await _userRepository.GetUserByEmailAsync(request.Email);
            if (existingUser != null)
                return BadRequest("Email already in use.");

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = request.Password
            };

            var createdUser = await _userRepository.CreateUserAsync(user);

            var response = new AuthUserResponse
            {
                Id = createdUser.Id,
                Username = createdUser.Username,
                Email = createdUser.Email
            };

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            if (user == null || user.PasswordHash != request.Password)
                return Unauthorized("Invalid credentials.");

            var response = new AuthUserResponse
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };

            return Ok(response);
        }
    }
}