// Owner 1: "Juan Pablo Ordonez Gomez" has added 73% of the code in this file
using Mealventory.Core.Interfaces;
using Mealventory.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Mealventory.API.Controllers
{
    /// Handles user registration and login endpoints.
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        /// Field to store the user repository dependency.
        private readonly IUserRepository _userRepository;

        /// Method to create an authentication controller with required dependencies.
        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// Method to register a new user account.
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

        /// Method to authenticate a user with email and password.
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