// Owner 1: "Daniel Bajenov" has added 36.36% of the code in this file
// Owner 1: "Juan Pablo Ordonez Gomez" has added 63.64% of the code in this file

using Mealventory.Core.Models;

namespace Mealventory.Web.Services
{
    public class FoodApiService(HttpClient httpClient)
    {
        public Task<List<FoodItem>?> GetItemsAsync(int userId)
        {
            return httpClient.GetFromJsonAsync<List<FoodItem>>($"api/food?userId={userId}");
        }

        public async Task<List<FoodItem>> GetItemsByLocationAsync(int userId, string location)
        {
            var items = await httpClient.GetFromJsonAsync<List<FoodItem>>($"api/food?userId={userId}")
                        ?? new List<FoodItem>();

            return items
                .Where(x => string.Equals(x.Location, location, StringComparison.OrdinalIgnoreCase))
                .OrderBy(x => x.ExpirationDate)
                .ToList();
        }

        public Task<HttpResponseMessage> AddItemAsync(FoodItem item)
        {
            return httpClient.PostAsJsonAsync("api/food", item);
        }

        public Task<HttpResponseMessage> DeleteItemAsync(int id, int userId)
        {
            return httpClient.DeleteAsync($"api/food/{id}?userId={userId}");
        }
    }
}