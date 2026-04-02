using Mealventory.Core.Models;

namespace Mealventory.Web.Services
{
    public class FoodApiService(HttpClient httpClient)
    {
        public Task<List<FoodItem>?> GetItemsAsync(int userId)
        {
            return httpClient.GetFromJsonAsync<List<FoodItem>>($"api/food?userId={userId}");
        }

        public Task<HttpResponseMessage> AddItemAsync(FoodItem item)
        {
            return httpClient.PostAsJsonAsync("api/food", item);
        }

        public Task<HttpResponseMessage> UpdateQuantityAsync(int id, int userId, int quantity)
        {
            return httpClient.PatchAsJsonAsync($"api/food/{id}/quantity", new UpdateFoodQuantityRequest
            {
                UserId = userId,
                Quantity = quantity
            });
        }

        public Task<HttpResponseMessage> DeleteItemAsync(int id, int userId)
        {
            return httpClient.DeleteAsync($"api/food/{id}?userId={userId}");
        }
    }
}
