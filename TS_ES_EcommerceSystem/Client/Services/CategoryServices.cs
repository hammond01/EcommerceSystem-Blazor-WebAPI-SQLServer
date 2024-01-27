using Client.Helpers;
using Models;
using System.Text.Json;

namespace Client.Services
{
    public class CategoryServices
    {
        private readonly HttpClient _client;
        public CategoryServices(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }
        public async Task<List<Categories>> GetCategories()
        {
            var request = await _client.GetAsync($"v1/Category/gets");

            if (request.IsSuccessStatusCode)
            {
                var jsonString = await request.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(jsonString).RootElement;

                if (json.GetProperty("status").GetInt16() == 200)
                {
                    var r = json.GetProperty("data").GetObject<List<Categories>>();

                    return r;
                }
            }

            return new List<Categories>();
        }
    }
}
