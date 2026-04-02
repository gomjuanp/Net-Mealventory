using Mealventory.Core.Models;

namespace Mealventory.Web.Services
{
    public class AuthApiService(HttpClient httpClient)
    {
        public Task<HttpResponseMessage> LoginAsync(LoginRequest request)
        {
            return httpClient.PostAsJsonAsync("api/auth/login", request);
        }

        public Task<HttpResponseMessage> RegisterAsync(User user)
        {
            return httpClient.PostAsJsonAsync("api/auth/register", user);
        }
    }
}
