// Owner 1: "Juan Pablo Ordonez Gomez" has added 100% of the code in this file
using Mealventory.API.Database;
using Mealventory.Core.Interfaces;
using Mealventory.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Mealventory.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MealventoryDbContext _context;

        public UserRepository(MealventoryDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}