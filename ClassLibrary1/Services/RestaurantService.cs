using HTTPClassLib.Models;
using HTTPClassLib.Models.Response;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace HTTPClassLib.Services
{
    public class RestaurantService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public RestaurantService(string baseUrl, string username, string password)
        {
            _baseUrl = baseUrl;
            _httpClient = new HttpClient();

            var authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);
        }

        //i suggest that it's a POST request
        public async Task<List<Dish>> GetMenuItemsAsync()
        {
            var result = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_baseUrl}/getMenu", new ApiRequest(){
                Command = "GetMenu", CommandParameters = new Dictionary<string, bool>()
                {
                    { "WithPrice", true }
                }});
            return await ProcessResponse<List<Dish>>(result);
        }

        public async Task<bool> PostOrderAsync(Order order)
        {
            var jsonContent = JsonSerializer.Serialize(order);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_baseUrl}/sendOrder", content);
            return (await ProcessResponse<object>(response)) != null;
        }

        private async Task<T> ProcessResponse<T>(HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadFromJsonAsync<ApiResponse<T>>();
            if (responseContent == null || !responseContent.Success)
            {
                throw new Exception(responseContent?.ErrorMessage ?? "Unknown error");
            }

            return responseContent.Data;
        }
    }
}
