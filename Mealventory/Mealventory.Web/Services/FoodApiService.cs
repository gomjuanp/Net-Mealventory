// Owner 1: "Daniel Bajenov" has added 36.36% of the code in this file
// Owner 2: "Juan Pablo Ordonez Gomez" has added 63.64% of the code in this file
// Principal Authors: Juan Pablo Ordonez Gomez, Daniel Bajenov
// Description: Client-side service used by Blazor components to call the server Food API.

using Mealventory.Core.Models;

namespace Mealventory.Web.Services
{
    /// <summary>
    /// Service that wraps HTTP calls to the Food API.
    /// </summary>
    public class FoodApiService(HttpClient httpClient)
    {
        /// <summary>
        /// Gets all food items for a user.
        /// </summary>
        /// <param name="userId">User id.</param>
        public Task<List<FoodItem>?> GetItemsAsync(int userId)
        {
            return httpClient.GetFromJsonAsync<List<FoodItem>>($"api/food?userId={userId}");
        }

        /// <summary>
        /// Gets items filtered by location and ordered by expiration date.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <param name="location">Location string (e.g., Fridge).</param>
        public async Task<List<FoodItem>> GetItemsByLocationAsync(int userId, string location)
        {
            var items = await httpClient.GetFromJsonAsync<List<FoodItem>>($"api/food?userId={userId}")
                        ?? new List<FoodItem>();

            return items
                .Where(x => string.Equals(x.Location, location, StringComparison.OrdinalIgnoreCase))
                .OrderBy(x => x.ExpirationDate)
                .ToList();
        }

        /// <summary>
        /// Adds a new food item via the API.
        /// </summary>
        /// <param name="item">Item to add.</param>
        public Task<HttpResponseMessage> AddItemAsync(FoodItem item)
        {
            return httpClient.PostAsJsonAsync("api/food", item);
        }

        /// <summary>
        /// Deletes a food item via the API.
        /// </summary>
        public Task<HttpResponseMessage> DeleteItemAsync(int id, int userId)
        {
            return httpClient.DeleteAsync($"api/food/{id}?userId={userId}");
        }
    }
}