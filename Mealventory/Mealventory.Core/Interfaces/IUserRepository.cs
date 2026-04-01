using Mealventory.Core.Models;
using System.Threading.Tasks;

namespace Mealventory.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<User> CreateUserAsync(User user);
        Task<User?> GetUserByIdAsync(int id);
    }
}