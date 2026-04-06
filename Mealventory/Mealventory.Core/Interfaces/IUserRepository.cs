// Owner 1: "Juan Pablo Ordonez Gomez" has added 100% of the code in this file
using Mealventory.Core.Models;
using System.Threading.Tasks;

namespace Mealventory.Core.Interfaces
{
    /// Defines operations to manage user data and lookups.
    public interface IUserRepository
    {
        /// Method to get a user by email address.
        Task<User?> GetUserByEmailAsync(string email);

        /// Method to create a new user.
        Task<User> CreateUserAsync(User user);

        /// Method to get a user by identifier.
        Task<User?> GetUserByIdAsync(int id);
    }
}