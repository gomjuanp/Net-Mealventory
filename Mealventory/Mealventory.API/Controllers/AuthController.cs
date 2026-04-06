// Owner 1: "Juan Pablo Ordonez Gomez" has added 74% of the code in this file
// Owner 2: "Daniel Bajenov" has added 26% of the code in this file
// Principal Author: Juan Pablo Ordonez Gomez
// Description: Controller responsible for handling user authentication (register and login) API endpoints.
using Mealventory.Core.Interfaces;
using Mealventory.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Mealventory.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    /// <summary>
    /// Controller for user authentication endpoints (register, login).
    /// </summary>
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// Repository used to access and manage user data.
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Creates a new instance of <see cref="AuthController"/>.
        /// </summary>
        /// <param name="userRepository">Repository for user data.</param>
        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="request">The registration request containing username, email and password.</param>
        /// <returns>HTTP 200 with the created user info or 400/other status on error.</returns>
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

        /// <summary>
        /// Logs a user in using email and password.
        /// </summary>
        /// <param name="request">The login request containing email and password.</param>
        /// <returns>HTTP 200 with user info when successful or 401 on failure.</returns>
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