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
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(user.Email);
            if (existingUser != null)
                return BadRequest("Email already in use.");

            // Extremely basic hash for MVP. Note: In a real app use BCrypt/Argon2.
            user.PasswordHash = user.PasswordHash;

            var createdUser = await _userRepository.CreateUserAsync(user);
            return Ok(createdUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            if (user == null || user.PasswordHash != request.Password)
                return Unauthorized("Invalid credentials.");

            return Ok(user);
        }
    }

    public class LoginRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}