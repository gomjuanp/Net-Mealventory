// Owner 1: "Juan Pablo Ordonez Gomez" has added 100% of the code in this file
using Mealventory.API.Database;
using Mealventory.Core.Interfaces;
using Mealventory.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Mealventory.API.Repositories
{
    /// Provides data access operations for user entities.
    public class UserRepository : IUserRepository
    {
        /// Field to store the database context dependency.
        private readonly MealventoryDbContext _context;

        /// Method to create a user repository with required dependencies.
        public UserRepository(MealventoryDbContext context)
        {
            _context = context;
        }

        /// Method to get a user by email address.
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        /// Method to create a new user.
        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        /// Method to get a user by identifier.
        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}